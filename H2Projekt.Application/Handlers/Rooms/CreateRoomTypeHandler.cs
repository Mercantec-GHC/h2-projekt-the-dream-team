using FluentValidation;
using H2Projekt.Application.Commands.Rooms;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;

namespace H2Projekt.Application.Handlers.Rooms
{
    public class CreateRoomTypeHandler
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IValidator<RoomType> _validator;

        public CreateRoomTypeHandler(IRoomRepository roomRepository, IValidator<RoomType> validator)
        {
            _roomRepository = roomRepository;
            _validator = validator;
        }

        public async Task<int> Handle(CreateRoomTypeCommand request, CancellationToken cancellationToken = default)
        {
            var roomTypeExists = await _roomRepository.RoomTypeExistsAsync(request.Name, cancellationToken);

            if (roomTypeExists)
            {
                throw new DuplicateException($"Room type with name {request.Name} already exists.");
            }

            var roomType = new RoomType(request.Name, request.Description, request.MaxOccupancy);

            var validationResult = await _validator.ValidateAsync(roomType);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.ToString("\n"));
            }

            await _roomRepository.AddRoomTypeAsync(roomType, cancellationToken);

            return roomType.Id;
        }
    }
}
