using H2Projekt.Application.Commands.Rooms;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;

namespace H2Projekt.Application.Handlers.Rooms
{
    public class UpdateRoomHandler
    {
        private readonly IRoomRepository _roomRepository;

        public UpdateRoomHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task HandleAsync(UpdateRoomCommand request, CancellationToken cancellationToken = default)
        {
            var existingRoom = await _roomRepository.GetRoomByNumberAsync(request.Number, cancellationToken);

            if (existingRoom is null)
            {
                throw new NonExistentException($"Room with number {request.Number} doesn't exist.");
            }

            existingRoom.UpdateDetails(request.Number, request.RoomTypeId, request.Status);

            await _roomRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
