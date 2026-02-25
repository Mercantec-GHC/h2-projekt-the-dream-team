using FluentValidation;
using H2Projekt.Domain.Enums;
using H2Projekt.Domain.Validators.Rooms;

namespace H2Projekt.Domain
{
    public class Room : EntityBase
    {
        public string Number { get; private set; } = default!;

        public int RoomTypeId { get; private set; }
        public RoomType RoomType { get; private set; } = default!;

        public RoomAvailabilityStatus Status { get; private set; }
        public Booking Booking { get; private set; }

        public Room() { }

        public Room(string number, int roomTypeId, RoomAvailabilityStatus status)
        {
            Number = number;
            RoomTypeId = roomTypeId;
            Status = status;

            ThrowIfInvalid();
        }

        public void UpdateDetails(string number, int roomTypeId, RoomAvailabilityStatus status)
        {
            Number = number;
            RoomTypeId = roomTypeId;
            Status = status;

            ThrowIfInvalid();
        }

        public void UpdateStatus(RoomAvailabilityStatus status)
        {
            Status = status;

            ThrowIfInvalid();
        }

        private void ThrowIfInvalid()
        {
            var validator = new RoomValidator();

            var result = validator.Validate(this);

            if (result.IsValid)
            {
                return;
            }

            throw new ValidationException(result.Errors);
        }
    }
}
