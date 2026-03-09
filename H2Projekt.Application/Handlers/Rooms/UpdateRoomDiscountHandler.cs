using H2Projekt.Application.Commands.Rooms;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;

namespace H2Projekt.Application.Handlers.Rooms
{
    public class UpdateRoomDiscountHandler
    {
        private readonly IRoomRepository _roomRepository;

        public UpdateRoomDiscountHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task HandleAsync(UpdateRoomDiscountCommand request, CancellationToken cancellationToken)
        {
            var roomType = await _roomRepository.GetRoomTypeByIdAsync(request.RoomTypeId, cancellationToken);

            if (roomType is null)
            {
                throw new NonExistentException($"Room type with ID {request.RoomTypeId} doesn't exist.");
            }

            var roomDiscount = roomType.RoomDiscounts.FirstOrDefault(room => room.Id == request.Id);

            if (roomDiscount is null)
            {
                throw new NonExistentException($"Room discount with ID {request.Id} doesn't exist.");
            }

            roomDiscount.UpdateDetails(request.RoomTypeId, request.Description, request.FromDate, request.ToDate, request.PricePerNight);

            await _roomRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
