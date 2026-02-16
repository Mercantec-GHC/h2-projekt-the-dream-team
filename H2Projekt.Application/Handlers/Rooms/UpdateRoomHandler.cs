using FluentValidation;
using H2Projekt.Application.Commands.Rooms;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;

namespace H2Projekt.Application.Handlers.Rooms
{
    public class UpdateRoomHandler
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IValidator<Room> _validator;

        public UpdateRoomHandler(IRoomRepository roomRepository, IValidator<Room> validator)
        {
            _roomRepository = roomRepository;
            _validator = validator;
        }

        public async Task HandleAsync(UpdateRoomCommand request, CancellationToken cancellationToken = default)
        {
            var existingRoom = await _roomRepository.GetRoomByNumberAsync(request.Number, cancellationToken);

            if (existingRoom is null)
            {
                throw new NonExistentException($"Room with number {request.Number} doesn't exist.");
            }

            existingRoom.UpdateDetails(request.Number, request.RoomTypeId, request.Status);

            var validationResult = await _validator.ValidateAsync(existingRoom);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            await _roomRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
