using H2Projekt.Application.Commands.Rooms;
using H2Projekt.Application.Dto.Rooms;
using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;

namespace H2Projekt.Application.Handlers.Rooms
{
    public class GetAvailableRoomTypesForStayHandler
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingRepository _bookingRepository;

        public GetAvailableRoomTypesForStayHandler(IRoomRepository roomRepository, IBookingRepository bookingRepository)
        {
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task<AvailableRoomTypesForStayDto> HandleAsync(GetAvailableRoomTypesForStayCommand request, CancellationToken cancellationToken = default)
        {
            // Get all rooms
            var rooms = await _roomRepository.GetAllRoomsAsync(cancellationToken);

            // Get all room types
            var roomTypes = await _roomRepository.GetAllRoomTypesAsync(cancellationToken);

            // Get all bookings
            var bookings = await _bookingRepository.GetAllBookingsAsync(cancellationToken);

            // Create a function to check if there are any available rooms for a given room type based on the requested stay dates
            var IsRoomTypeAvailableForStay = new Func<RoomType, bool>(roomType =>
            {
                // Check if there are any rooms for the given room type
                var hasRoomsForRoomType = rooms.Any(room => room.RoomTypeId == roomType.Id);

                // Get the amount of rooms for the given room type
                var amountOfRoomsForRoomType = rooms.Count(room => room.RoomTypeId == roomType.Id);

                // Get the amount of bookings for the given room type that overlap with the requested stay dates
                var amountOfBookingsForRoomType = bookings.Count(booking => booking.RoomTypeId == roomType.Id && booking.FromDate <= request.ToDate && booking.ToDate >= request.FromDate);

                // If there are more rooms for the given room type than bookings for the given room type, then there are available rooms for the given room type
                return hasRoomsForRoomType && amountOfBookingsForRoomType != amountOfRoomsForRoomType;
            });

            // Find the best suitable room types based on the number of guests 
            var initialBestSuitableRoomTypes = roomTypes.Where(roomType => roomType.MaxOccupancy == request.NumberOfAdults + request.NumberOfChildren);

            // Create a variable to keep track of the suitable room types, starting with the best suitable room types based on the number of guests and filtering out the room types that are not available for the requested stay dates
            var suitableRoomTypes = initialBestSuitableRoomTypes.Where(roomType => IsRoomTypeAvailableForStay(roomType));

            // If there are any suitable room types, try to find the next best suitable room types based on the number of guests and the maximum room type id of the current suitable room types until there are available rooms for the current suitable room types or there are no more suitable room types to check
            if (initialBestSuitableRoomTypes.Any())
            {
                // Create a variable to keep track of the maximum room type id of the current suitable room types, starting with the maximum room type id of the initial best suitable room types or 1 if there are no initial best suitable room types
                var maxRoomTypeId = suitableRoomTypes.Any() ? suitableRoomTypes.Max(roomType => roomType.Id) : 1;

                // Create a variable to keep track of the number of guests for the current suitable room types, starting with the number of guests for the initial best suitable room types
                var guests = request.NumberOfAdults + request.NumberOfChildren;

                // Aslong as there are no available rooms for the current suitable room types, try to find the next best suitable room types
                while (!suitableRoomTypes.Any() && guests <= roomTypes.Max(roomType => roomType.MaxOccupancy))
                {
                    // Find the next best suitable room type based on the number of guests 
                    suitableRoomTypes = roomTypes
                        .Where(roomType =>
                            IsRoomTypeAvailableForStay(roomType) &&
                            roomType.MaxOccupancy == guests &&
                            roomType.Id > maxRoomTypeId)
                        .ToList();

                    // Increment the number of guests to find the next best suitable room types based on the number of guests
                    guests++;
                }
            }

            // Map the suitable room types to RoomTypeDto and return the list of RoomTypeDto, setting the PricePerNight to the maximum price per night of the initial best suitable room types
            return new AvailableRoomTypesForStayDto()
            {
                SuitableRoomTypes = suitableRoomTypes
                    .Select(suitableRoomType => new RoomTypeDto(suitableRoomType)
                    {
                        PricePerNight = initialBestSuitableRoomTypes.Max(roomType => roomType.PricePerNight),
                    })
                    .ToList(),
                OtherRoomTypes = roomTypes
                    .Where(roomType => IsRoomTypeAvailableForStay(roomType) && !suitableRoomTypes.Any(suitableRoomType => roomType.Id == suitableRoomType.Id))
                    .Select(roomType => new RoomTypeDto(roomType))
                    .ToList(),
                UnavailableRoomTypes = roomTypes
                    .Where(roomType => !IsRoomTypeAvailableForStay(roomType))
                    .Select(roomType => new RoomTypeDto(roomType))
                    .ToList()
            };
        }
    }
}
