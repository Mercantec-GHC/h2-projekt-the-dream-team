using System;
using System.Collections.Generic;
using System.Text;
using H2Projekt.Domain.Enums;

namespace H2Projekt.Domain
{
    public class Reservation
    {
        public int Id { get; set; }
        public Guest Guest { get; set; } = default!;
        public RoomTypeEnum RoomType { get; set; }
        public DateTimeOffset FromDate { get; set; }
        public DateTimeOffset ToDate { get; set; }

        public Reservation() { }
        public Reservation(Guest guest, RoomTypeEnum roomType, DateTimeOffset checkInDate, DateTimeOffset checkOutDate)
        {
            Guest = guest;
            RoomType = roomType;
            FromDate = checkInDate;
            ToDate = checkOutDate;
        }
    }
}