using H2Projekt.Application.Commands;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;

namespace H2Projekt.Application.Handlers
{
    public class CreateRoomHandler
    {
        private readonly IRoomRepository _roomRepository;

        public CreateRoomHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<int> Handle(CreateRoomCommand request, CancellationToken cancellationToken = default)
        {
            var existingRoom = await _roomRepository.GetRoomByRoomNumberAsync(request.Number, cancellationToken);

            if (existingRoom is not null)
            {
                throw new RoomDuplicateException($"Room with number {request.Number} already exists.");
            }

            var room = new Room(request.Number, request.Capacity, request.PricePerNight);

            await _roomRepository.AddAsync(room, cancellationToken);

            return room.Id;
        }
    }
}
