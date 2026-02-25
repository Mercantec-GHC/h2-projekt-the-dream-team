using H2Projekt.Application.Commands.Rooms;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;

namespace H2Projekt.Application.Handlers.Rooms
{
    public class CreateRoomHandler
    {
        private readonly IRoomRepository _roomRepository;

        public CreateRoomHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<int> HandleAsync(CreateRoomCommand request, CancellationToken cancellationToken = default)
        {
            var roomType = await _roomRepository.GetRoomTypeByIdAsync(request.RoomTypeId, cancellationToken);

            if (roomType is null)
            {
                throw new NonExistentException($"Room type with ID {request.RoomTypeId} doesn't exist.");
            }

            var roomExists = roomType.Rooms.Any(room => room.Number == request.Number);

            if (roomExists)
            {
                throw new DuplicateException($"Room with number {request.Number} already exists.");
            }

            var room = new Room(request.Number, request.RoomTypeId, request.Status);

            roomType.AddRoom(room);

            await _roomRepository.SaveChangesAsync(cancellationToken);

            return room.Id;
        }
    }
}
