using H2Projekt.Application.Commands;
using H2Projekt.Application.Interfaces;

namespace H2Projekt.Application.Handlers
{
    public class UpdateRoomHandler
    {
        private readonly IRoomRepository _roomRepository;

        public UpdateRoomHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task Handle(UpdateRoomCommand request, CancellationToken cancellationToken = default)
        {
            var existingRoom = await _roomRepository.GetRoomByRoomNumberAsync(request.Number, cancellationToken);

            if (existingRoom is null)
            {
                throw new InvalidOperationException($"Room with number {request.Number} doesn't exist.");
            }

            existingRoom.UpdateDetails(request.Number, request.Capacity, request.PricePerNight);

            await _roomRepository.UpdateAsync(existingRoom, cancellationToken);
        }
    }
}
