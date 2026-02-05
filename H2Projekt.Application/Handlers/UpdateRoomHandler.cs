using FluentValidation;
using H2Projekt.Application.Commands;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;

namespace H2Projekt.Application.Handlers
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

        public async Task Handle(UpdateRoomCommand request, CancellationToken cancellationToken = default)
        {
            var existingRoom = await _roomRepository.GetRoomByRoomNumberAsync(request.Number, cancellationToken);

            if (existingRoom is null)
            {
                throw new RoomNonExistentException($"Room with number {request.Number} doesn't exist.");
            }

            existingRoom.UpdateDetails(request.Number, request.Capacity, request.PricePerNight);

            var validationResult = await _validator.ValidateAsync(existingRoom);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.ToString("\n"));
            }

            await _roomRepository.UpdateAsync(existingRoom, cancellationToken);
        }
    }
}
