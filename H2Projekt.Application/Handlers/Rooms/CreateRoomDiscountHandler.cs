using H2Projekt.Application.Commands.Rooms;
using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;

namespace H2Projekt.Application.Handlers.Rooms
{
    public class CreateRoomDiscountHandler
    {
        private readonly IRoomRepository _roomRepository;

        public CreateRoomDiscountHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<int> HandleAsync(CreateRoomDiscountCommand request, CancellationToken cancellationToken = default)
        {
            var roomDiscount = new RoomDiscount(request.RoomTypeId, request.Description, request.FromDate, request.ToDate, request.PricePerNight);

            await _roomRepository.AddRoomDiscountAsync(roomDiscount, cancellationToken);

            return roomDiscount.Id;
        }
    }
}
