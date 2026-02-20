using H2Projekt.Application.Dto.Rooms;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;

namespace H2Projekt.Application.Handlers.Rooms
{
    public class GetRoomByNumberHandler
    {
        private readonly IRoomRepository _roomRepository;

        public GetRoomByNumberHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<RoomDto> HandleAsync(string number, CancellationToken cancellationToken = default)
        {
            var room = await _roomRepository.GetRoomByNumberAsync(number, cancellationToken);

            if (room is null)
            {
                throw new NonExistentException($"Room with number {number} doesn't exist.");
            }

            return new RoomDto(room);
        }
    }
}
