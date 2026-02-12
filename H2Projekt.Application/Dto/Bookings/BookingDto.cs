using H2Projekt.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace H2Projekt.Application.Dto.Bookings
{
    public class BookingDto
    {
        public Guest Guest { get; private set; } = default!;
        public RoomType RoomType { get; set; } = default!;
        public DateOnly FromDate { get; set; } 
        public DateOnly ToDate { get; set; }
        public decimal PriceLocked { get; set; }
        public Room? AssignedRoom { get; set; }

        public BookingDto(Booking booking)
        {
            Guest = booking.Guest;
            RoomType = booking.RoomType;
            FromDate = booking.FromDate;
            ToDate = booking.ToDate;
            PriceLocked = booking.PriceLocked;
            AssignedRoom = booking.AssignedRoom;
        }
    }
}
