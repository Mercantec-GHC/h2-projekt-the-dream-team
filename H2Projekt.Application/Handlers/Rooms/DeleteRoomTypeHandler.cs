using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;

namespace H2Projekt.Application.Handlers.Rooms
{
    public class DeleteRoomTypeHandler
    {
        private readonly IRoomRepository _roomRepository;

        public DeleteRoomTypeHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task Handle(int id, CancellationToken cancellationToken = default)
        {
            var roomType = await _roomRepository.GetRoomTypeByIdAsync(id, cancellationToken);

            if (roomType is null)
            {
                throw new NonExistentException($"Room type with id {id} doesn't exist.");
            }

            await _roomRepository.DeleteRoomTypeAsync(roomType, cancellationToken);
        }
    }
}
