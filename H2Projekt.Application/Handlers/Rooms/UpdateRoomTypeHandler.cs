using FluentValidation;
using H2Projekt.Application.Commands.Rooms;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;

namespace H2Projekt.Application.Handlers.Rooms
{
    public class UpdateRoomTypeHandler
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IValidator<RoomType> _validator;

        public UpdateRoomTypeHandler(IRoomRepository roomRepository, IValidator<RoomType> validator)
        {
            _roomRepository = roomRepository;
            _validator = validator;
        }

        public async Task Handle(UpdateRoomTypeCommand request, CancellationToken cancellationToken = default)
        {
            var existingRoomType = await _roomRepository.GetRoomTypeByIdAsync(request.Id, cancellationToken);

            if (existingRoomType is null)
            {
                throw new NonExistentException($"Room type with id {request.Id} doesn't exist.");
            }

            existingRoomType.UpdateDetails(request.Name, request.Description, request.MaxOccupancy);

            var validationResult = await _validator.ValidateAsync(existingRoomType);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.ToString("\n"));
            }

            await _roomRepository.UpdateRoomTypeAsync(existingRoomType, cancellationToken);
        }
    }
}
