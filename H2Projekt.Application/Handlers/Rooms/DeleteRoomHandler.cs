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

        public async Task HandleAsync(string number, CancellationToken cancellationToken = default)
        {
            var room = await _roomRepository.GetRoomByNumberAsync(number, cancellationToken);

            if (room is null)
            {
                throw new NonExistentException($"Room with number {number} doesn't exist.");
            }

            await _roomRepository.DeleteRoomAsync(room, cancellationToken);
        }
    }
}
