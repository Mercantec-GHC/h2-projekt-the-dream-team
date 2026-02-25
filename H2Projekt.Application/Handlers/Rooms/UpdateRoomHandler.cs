using H2Projekt.Application.Commands.Rooms;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;

namespace H2Projekt.Application.Handlers.Rooms
{
    public class UpdateRoomHandler
    {
        private readonly IRoomRepository _roomRepository;

        public UpdateRoomHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task HandleAsync(UpdateRoomCommand request, CancellationToken cancellationToken = default)
        {
            var roomType = await _roomRepository.GetRoomTypeByIdAsync(request.RoomTypeId, cancellationToken);

            if (roomType is null)
            {
                throw new NonExistentException($"Room type with ID {request.RoomTypeId} doesn't exist.");
            }

            var room = roomType.Rooms.FirstOrDefault(room => room.Number == request.Number);

            if (room is null)
            {
                throw new NonExistentException($"Room with number {request.Number} doesn't exist.");
            }

            room.UpdateDetails(request.Number, request.RoomTypeId, request.Status);

            await _roomRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
