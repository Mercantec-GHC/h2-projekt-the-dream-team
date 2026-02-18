using FluentValidation;
using H2Projekt.Application.Commands.Rooms;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;

namespace H2Projekt.Application.Handlers.Rooms
{
    public class UpdateRoomDiscountHandler
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IValidator<RoomDiscount> _validator;

        public UpdateRoomDiscountHandler(IRoomRepository roomRepository, IValidator<RoomDiscount> validator)
        {
            _roomRepository = roomRepository;
            _validator = validator;
        }

        public async Task HandleAsync(UpdateRoomDiscountCommand request, CancellationToken cancellationToken = default)
        {
            var existingRoomDiscount = await _roomRepository.GetRoomDiscountByIdAsync(request.Id, cancellationToken);

            if (existingRoomDiscount is null)
            {
                throw new NonExistentException($"Room discount with id {request.Id} doesn't exist.");
            }

            existingRoomDiscount.UpdateDetails(request.RoomTypeId, request.Description, request.FromDate, request.ToDate, request.PricePerNight);

            var validationResult = await _validator.ValidateAsync(existingRoomDiscount);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            await _roomRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
