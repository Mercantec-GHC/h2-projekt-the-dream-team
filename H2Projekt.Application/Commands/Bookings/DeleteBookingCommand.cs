using System;
using System.Collections.Generic;
using System.Text;

namespace H2Projekt.Application.Commands.Bookings
{
    public class DeleteBookingCommand
    {
        public int GuestId { get; set; }
            public int BookingId { get; set; }
    }
}
