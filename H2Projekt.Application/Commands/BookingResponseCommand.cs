using H2Projekt.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace H2Projekt.Application.Commands
{
    public class BookingResponseCommand
    {
        public Guid BookingId { get; set; }
        public RoomType RoomType { get; set; }
        public DateTimeOffset FromDate { get; set; }
        public DateTimeOffset ToDate { get; set; }
        public Guid? AssignedRoomId { get; set; }
    }
}
