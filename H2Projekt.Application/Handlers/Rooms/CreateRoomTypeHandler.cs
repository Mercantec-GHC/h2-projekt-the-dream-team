using H2Projekt.Application.Commands.Rooms;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;

namespace H2Projekt.Application.Handlers.Rooms
{
    public class CreateRoomTypeHandler
    {
        private readonly IRoomRepository _roomRepository;

        public CreateRoomTypeHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<int> HandleAsync(CreateRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var roomTypeExists = await _roomRepository.RoomTypeExistsAsync(request.Name, cancellationToken);

            if (roomTypeExists)
            {
                throw new DuplicateException($"Room type with name {request.Name} already exists.");
            }

            var roomType = new RoomType(request.Name, request.Description, request.MaxOccupancy, request.PetsAllowed, request.PricePerNight);

            await _roomRepository.AddRoomTypeAsync(roomType, cancellationToken);

            return roomType.Id;
        }
    }
}
