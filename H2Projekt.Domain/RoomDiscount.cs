using FluentValidation;
using H2Projekt.Domain.Validators.Rooms;

namespace H2Projekt.Domain
{
    public class RoomDiscount : EntityBase
    {
        public int RoomTypeId { get; private set; }
        public RoomType RoomType { get; private set; } = default!;

        public string Description { get; private set; }

        public DateOnly FromDate { get; private set; }
        public DateOnly ToDate { get; private set; }

        public int PricePerNight { get; private set; }

        public RoomDiscount() { }

        public RoomDiscount(int roomTypeId, string description, DateOnly fromDate, DateOnly toDate, int pricePerNight)
        {
            RoomTypeId = roomTypeId;
            Description = description;
            FromDate = fromDate;
            ToDate = toDate;
            PricePerNight = pricePerNight;

            ThrowIfInvalid();
        }

        public void UpdateDetails(int roomTypeId, string description, DateOnly fromDate, DateOnly toDate, int pricePerNight)
        {
            RoomTypeId = roomTypeId;
            Description = description;
            FromDate = fromDate;
            ToDate = toDate;
            PricePerNight = pricePerNight;

            ThrowIfInvalid();
        }

        private void ThrowIfInvalid()
        {
            var validator = new RoomDiscountValidator();

            var result = validator.Validate(this);

            if (result.IsValid)
            {
                return;
            }

            throw new ValidationException(result.Errors);
        }
    }
}
