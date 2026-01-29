using H2Projekt.Application.Commands;
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
            var room = new Room(request.Number, request.Capacity, request.PricePerNight);

            await _roomRepository.AddAsync(room, cancellationToken);

            return room.Id;
        }
    }
}
