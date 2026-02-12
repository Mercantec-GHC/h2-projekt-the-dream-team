using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace H2Projekt.Application.Handlers.Bookings
{
    public class GetAllBookingsHandler
    {
        private readonly IBookingRepository _repository;

        public GetAllBookingsHandler(IBookingRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Booking>> Handle(CancellationToken cancellationToken = default)
        {
            var bookings = await _repository.GetAllBookingsAsync(cancellationToken);

            return bookings;
        }
    }
}