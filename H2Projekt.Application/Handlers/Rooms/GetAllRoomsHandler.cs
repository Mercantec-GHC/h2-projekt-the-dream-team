using H2Projekt.Application.Dto.Rooms;
using H2Projekt.Application.Interfaces;

namespace H2Projekt.Application.Handlers.Rooms
{
    public class GetAllRoomsHandler
    {
        private readonly IRoomRepository _roomRepository;

        public GetAllRoomsHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<List<RoomDto>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var rooms = await _roomRepository.GetAllRoomsAsync(cancellationToken);

            return rooms.Select(room => new RoomDto(room)).ToList();
        }
    }
}
