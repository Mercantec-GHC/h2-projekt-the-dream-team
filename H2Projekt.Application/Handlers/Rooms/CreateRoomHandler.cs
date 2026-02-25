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
            var roomExists = await _roomRepository.RoomExistsAsync(request.Number, cancellationToken);

            if (roomExists)
            {
                throw new DuplicateException($"Room with number {request.Number} already exists.");
            }

            var room = new Room(request.Number, request.RoomTypeId, request.Status);

            await _roomRepository.AddRoomAsync(room, cancellationToken);

            return room.Id;
        }
    }
}
