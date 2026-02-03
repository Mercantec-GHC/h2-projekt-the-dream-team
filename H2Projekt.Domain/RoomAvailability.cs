using System;
using System.Collections.Generic;
using System.Text;
using H2Projekt.Domain.Enums;

namespace H2Projekt.Domain
{
    public class RoomAvailability
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public DateOnly Date { get; set; }
        public RoomAvailabilityStatus Status { get; set; }
        public Room Room { get; set; } = default!; 
        public RoomAvailability() { } // Parameterless constructor for EF Core
        public RoomAvailability(int roomId, DateOnly date, RoomAvailabilityStatus status)
        {
            RoomId = roomId;
            Date = date;
            Status = status;
        }
        ublic void SetStatus(RoomAvailabilityStatus status) => Status = status;
    }
}
