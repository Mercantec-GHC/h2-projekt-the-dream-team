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

        public async Task HandleAsync(int id, CancellationToken cancellationToken = default)
        {
            var roomDiscount = await _roomRepository.GetRoomDiscountByIdAsync(id, cancellationToken);

            if (roomDiscount is null)
            {
                throw new NonExistentException($"Room discount with id {id} doesn't exist.");
            }

            await _roomRepository.DeleteRoomDiscountAsync(roomDiscount, cancellationToken);
        }
    }
}
