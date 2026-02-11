using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;

namespace H2Projekt.Application.Rooms.Handlers
{
    public class GetRoomByNumberHandler
    {
        private readonly IRoomRepository _roomRepository;

        public GetRoomByNumberHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<Room> Handle(string number, CancellationToken cancellationToken = default)
        {
            var room = await _roomRepository.GetRoomByNumberAsync(number, cancellationToken);

            if (room is null)
            {
                throw new NonExistentException($"Room with number {number} doesn't exist.");
            }

            return room;
        }
    }
}
