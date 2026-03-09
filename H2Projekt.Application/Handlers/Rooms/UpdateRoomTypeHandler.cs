using H2Projekt.Application.Commands.Rooms;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;

namespace H2Projekt.Application.Handlers.Rooms
{
    public class UpdateRoomTypeHandler
    {
        private readonly IRoomRepository _roomRepository;

        public UpdateRoomTypeHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task HandleAsync(UpdateRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var existingRoomType = await _roomRepository.GetRoomTypeByIdAsync(request.Id, cancellationToken);

            if (existingRoomType is null)
            {
                throw new NonExistentException($"Room type with id {request.Id} doesn't exist.");
            }

            existingRoomType.UpdateDetails(request.Name, request.Description, request.MaxOccupancy, request.PetsAllowed, request.PricePerNight);

            await _roomRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
