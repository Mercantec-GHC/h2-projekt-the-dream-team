using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;

namespace H2Projekt.Application.Handlers.Rooms
{
    public class GetAllRoomTypesHandler
    {
        private readonly IRoomRepository _roomRepository;

        public GetAllRoomTypesHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<List<RoomType>> Handle(CancellationToken cancellationToken = default)
        {
            var roomTypes = await _roomRepository.GetAllRoomTypesAsync(cancellationToken);

            return roomTypes;
        }
    }
}
