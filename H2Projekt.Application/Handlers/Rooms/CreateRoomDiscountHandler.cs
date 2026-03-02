using H2Projekt.Application.Commands.Rooms;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;

namespace H2Projekt.Application.Handlers.Rooms
{
    public class CreateRoomDiscountHandler
    {
        private readonly IRoomRepository _roomRepository;

        public CreateRoomDiscountHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<int> HandleAsync(CreateRoomDiscountCommand request, CancellationToken cancellationToken = default)
        {
            var roomType = await _roomRepository.GetRoomTypeByIdAsync(request.RoomTypeId, cancellationToken);

            if (roomType is null)
            {
                throw new NonExistentException($"Room type with ID {request.RoomTypeId} doesn't exist.");
            }

            var roomDiscount = new RoomDiscount(request.RoomTypeId, request.Description, request.FromDate, request.ToDate, request.PricePerNight);

            roomType.AddRoomDiscount(roomDiscount);

            await _roomRepository.SaveChangesAsync(cancellationToken);

            return roomDiscount.Id;
        }
    }
}
