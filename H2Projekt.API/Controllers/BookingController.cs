using FluentValidation;
using H2Projekt.Application.Commands.Bookings;
using H2Projekt.Application.Dto.Bookings;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Handlers.Bookings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace H2Projekt.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<BookingDto>>> GetAllBookings([FromServices] GetAllBookingsHandler handler)
        {
            var bookings = await handler.Handle();

            return Ok(bookings.Select(booking => new BookingDto(booking)));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookingDto?>> GetBookingById([FromServices] GetBookingByIdHandler handler, int id)
        {
            try
            {
                var booking = await handler.Handle(id);

                return Ok(new BookingDto(booking));
            }
            catch (NonExistentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<int>> CreateBooking([FromServices] CreateBookingHandler handler, [FromBody] CreateBookingCommand createBookingCommand)
        {
            try
            {
                var bookingId = await handler.HandleAsync(createBookingCommand);

                return Ok(bookingId);
            }
            catch (DuplicateException ex)
            {
                return Conflict(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteBooking([FromServices] DeleteBookingHandler handler, int guestId, int bookingId)
        {
            try
            {
                await handler.Handle(new DeleteBookingCommand()
                {
                    GuestId = guestId,
                    BookingId = bookingId
                });

                return Ok();
            }
            catch (NonExistentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
