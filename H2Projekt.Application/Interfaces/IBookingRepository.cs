using System;
using System.Collections.Generic;
using System.Text;
using H2Projekt.Domain;

namespace H2Projekt.Application.Interfaces
{
    public interface IBookingRepository
    {
        Task<IReadOnlyList<Booking>>GetBookingForRoomAsync(IEnumerable<Guid> roomIds, DateTimeOffset from, DateTimeOffset to);
        Task AddBookingAsync(Booking booking);
    }
}
