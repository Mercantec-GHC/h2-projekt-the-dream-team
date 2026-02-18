using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;

namespace H2Projekt.Application.Handlers.Rooms
{
    public class DeleteRoomHandler
    {
        private readonly IRoomRepository _roomRepository;

        public DeleteRoomHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task HandleAsync(int id, CancellationToken cancellationToken = default)
        {
            var room = await _roomRepository.GetRoomByIdAsync(id, cancellationToken);

            if (room is null)
            {
                throw new NonExistentException($"Room with id {id} doesn't exist.");
            }

            await _roomRepository.DeleteRoomAsync(room, cancellationToken);
        }
    }
}
