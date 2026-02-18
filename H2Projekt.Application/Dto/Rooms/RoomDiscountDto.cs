using H2Projekt.Domain;

namespace H2Projekt.Application.Dto.Rooms
{
    public class RoomDiscountDto
    {
        public int Id { get; set; }
        public RoomTypeDto RoomType { get; set; }
        public string Description { get; set; }
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        public int PricePerNight { get; set; }

        public RoomDiscountDto(RoomDiscount roomDiscount)
        {
            Id = roomDiscount.Id;
            RoomType = new RoomTypeDto(roomDiscount.RoomType);
            Description = roomDiscount.Description;
            FromDate = roomDiscount.FromDate;
            ToDate = roomDiscount.ToDate;
            PricePerNight = roomDiscount.PricePerNight;
        }
    }
}
