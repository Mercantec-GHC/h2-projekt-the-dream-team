using H2Projekt.Domain.Enums;

namespace H2Projekt.Application.Commands
{
    public class BookingResponseCommand
    {
        public Guid BookingId { get; set; }
        public RoomTypeEnum RoomType { get; set; }
        public DateTimeOffset FromDate { get; set; }
        public DateTimeOffset ToDate { get; set; }
        public Guid? AssignedRoomId { get; set; }
    }
}
