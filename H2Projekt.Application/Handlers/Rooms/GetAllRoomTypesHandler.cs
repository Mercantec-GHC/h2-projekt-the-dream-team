using H2Projekt.Application.Dto.Rooms;
using H2Projekt.Application.Interfaces;

namespace H2Projekt.Application.Handlers.Rooms
{
    public class GetAllRoomTypesHandler
    {
        private readonly IRoomRepository _roomRepository;

        public GetAllRoomTypesHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<List<RoomTypeDto>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var roomTypes = await _roomRepository.GetAllRoomTypesAsync(cancellationToken);

            return roomTypes.Select(roomType => new RoomTypeDto(roomType)).ToList();
        }
    }
}
