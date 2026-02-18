using FluentValidation;
using H2Projekt.Application.Commands.Rooms;
using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;

namespace H2Projekt.Application.Handlers.Rooms
{
    public class CreateRoomDiscountHandler
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IValidator<RoomDiscount> _validator;

        public CreateRoomDiscountHandler(IRoomRepository roomRepository, IValidator<RoomDiscount> validator)
        {
            _roomRepository = roomRepository;
            _validator = validator;
        }

        public async Task<int> HandleAsync(CreateRoomDiscountCommand request, CancellationToken cancellationToken = default)
        {
            var roomDiscount = new RoomDiscount(request.RoomTypeId, request.Description, request.FromDate, request.ToDate, request.PricePerNight);

            var validationResult = await _validator.ValidateAsync(roomDiscount);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            await _roomRepository.AddRoomDiscountAsync(roomDiscount, cancellationToken);

            return roomDiscount.Id;
        }
    }
}
