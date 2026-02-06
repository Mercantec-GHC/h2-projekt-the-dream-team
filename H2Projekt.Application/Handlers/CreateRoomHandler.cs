using FluentValidation;
using H2Projekt.Application.Commands;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;

namespace H2Projekt.Application.Handlers
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

        public async Task<int> Handle(CreateRoomCommand request, CancellationToken cancellationToken = default)
        {
            var roomExists = await _roomRepository.DoesRoomExistAsync(request.Number, cancellationToken);

            if (roomExists)
            {
                throw new RoomDuplicateException($"Room with number {request.Number} already exists.");
            }

            var room = new Room(request.Number, request.Type, request.PricePerNight);

            var validationResult = await _validator.ValidateAsync(room);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.ToString("\n"));
            }

            await _roomRepository.AddAsync(room, cancellationToken);

            return room.Id;
        }
    }
}
