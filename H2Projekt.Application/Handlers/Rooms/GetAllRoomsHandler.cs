using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;

namespace H2Projekt.Application.Handlers.Rooms
{
    public class GetAllRoomsHandler
    {
        private readonly IRoomRepository _roomRepository;

        public GetAllRoomsHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<List<Room>> Handle(CancellationToken cancellationToken = default)
        {
            var rooms = await _roomRepository.GetAllRoomsAsync(cancellationToken);

            return rooms;
        }
    }
}
