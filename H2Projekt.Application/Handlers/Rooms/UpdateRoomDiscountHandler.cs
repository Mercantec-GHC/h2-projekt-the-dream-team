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

        public async Task HandleAsync(UpdateRoomDiscountCommand request, CancellationToken cancellationToken = default)
        {
            var existingRoomDiscount = await _roomRepository.GetRoomDiscountByIdAsync(request.Id, cancellationToken);

            if (existingRoomDiscount is null)
            {
                throw new NonExistentException($"Room discount with id {request.Id} doesn't exist.");
            }

            existingRoomDiscount.UpdateDetails(request.RoomTypeId, request.Description, request.FromDate, request.ToDate, request.PricePerNight);

            await _roomRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
