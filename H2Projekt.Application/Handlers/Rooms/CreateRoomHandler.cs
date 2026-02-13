using FluentValidation;
using H2Projekt.Application.Commands.Rooms;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;

namespace H2Projekt.Application.Handlers.Rooms
{
    public class CreateRoomHandler
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IValidator<Room> _validator;

        public CreateRoomHandler(IRoomRepository roomRepository, IValidator<Room> validator)
        {
            _roomRepository = roomRepository;
            _validator = validator;
        }

        public async Task<int> HandleAsync(CreateRoomCommand request, CancellationToken cancellationToken = default)
        {
            var roomExists = await _roomRepository.RoomExistsAsync(request.Number, cancellationToken);

            if (roomExists)
            {
                throw new DuplicateException($"Room with number {request.Number} already exists.");
            }

            var room = new Room(request.Number, request.RoomTypeId, request.Status);

            var validationResult = await _validator.ValidateAsync(room);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.ToString("\n"));
            }

            await _roomRepository.AddRoomAsync(room, cancellationToken);

            return room.Id;
        }
    }
}
