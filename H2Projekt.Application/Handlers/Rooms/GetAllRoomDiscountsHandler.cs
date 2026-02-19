using H2Projekt.Application.Dto.Rooms;
using H2Projekt.Application.Interfaces;

namespace H2Projekt.Application.Handlers.Rooms
{
    public class GetAllRoomDiscountsHandler
    {
        private readonly IRoomRepository _roomRepository;

        public GetAllRoomDiscountsHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<List<RoomDiscountDto>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var roomDiscounts = await _roomRepository.GetAllRoomDiscountsAsync(cancellationToken);

            return roomDiscounts.Select(roomDiscount => new RoomDiscountDto(roomDiscount)).ToList();
        }
    }
}
