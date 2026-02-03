using H2Projekt.Application.Commands;
using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;

namespace H2Projekt.Application.Handlers
{
    public class DeleteRoomHandler
    {
        private readonly IRoomRepository _roomRepository;

        public DeleteRoomHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<bool> Handle(string number, CancellationToken cancellationToken = default)
        {
            var room = await _roomRepository.GetRoomByRoomNumberAsync(number, cancellationToken);

            if (room is null)
            {
                throw new InvalidOperationException($"Room with number {number} doesn't exist.");
            }

            var affectedRows = await _roomRepository.DeleteAsync(room, cancellationToken);

            return affectedRows > 0;
        }
    }
}
