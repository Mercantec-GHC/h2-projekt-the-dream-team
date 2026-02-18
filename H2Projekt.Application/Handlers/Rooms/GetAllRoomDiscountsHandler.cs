using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;

namespace H2Projekt.Application.Handlers.Rooms
{
    public class GetAllRoomDiscountsHandler
    {
        private readonly IRoomRepository _roomRepository;

        public GetAllRoomDiscountsHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<List<RoomDiscount>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var roomDiscounts = await _roomRepository.GetAllRoomDiscountsAsync(cancellationToken);

            return roomDiscounts;
        }
    }
}
