using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;

namespace H2Projekt.Application.Handlers.Rooms
{
    public class DeleteRoomDiscountHandler
    {
        private readonly IRoomRepository _roomRepository;

        public DeleteRoomDiscountHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task HandleAsync(int id, CancellationToken cancellationToken)
        {
            var roomDiscount = await _roomRepository.GetRoomDiscountByIdAsync(id, cancellationToken);

            if (roomDiscount is null)
            {
                throw new NonExistentException($"Room discount with ID {id} doesn't exist.");
            }

            roomDiscount.RoomType.RemoveRoomDiscount(roomDiscount);

            await _roomRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
