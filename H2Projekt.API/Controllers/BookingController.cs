using FluentValidation;
using H2Projekt.Application.Commands.Bookings;
using H2Projekt.Application.Dto.Bookings;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Handlers.Bookings;
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
            var bookings = await handler.HandleAsync();

            return Ok(bookings);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BookingOverviewDto>> GetBookingOverview([FromServices] GetBookingsOverviewHandler handler)
        {
            var result = await handler.HandleAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookingDto?>> GetBookingById([FromServices] GetBookingByIdHandler handler, int id)
        {
            try
            {
                var booking = await handler.HandleAsync(id);

                return Ok(booking);
            }
            catch (NonExistentException ex)
            {
                return NotFound(ex.GetProblemDetails());
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> CreateBooking([FromServices] CreateBookingHandler handler, [FromBody] CreateBookingCommand createBookingCommand)
        {
            try
            {
                var bookingId = await handler.HandleAsync(createBookingCommand);

                return Ok(bookingId);
            }
            catch (NonExistentException ex)
            {
                return NotFound(ex.GetProblemDetails());
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationProblemDetails(ex.Errors.ToDictionary()));
            }
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> AssignRoomToBooking([FromServices] AssignRoomToBookingHandler handler, int id)
        {
            try
            {
                var roomId = await handler.HandleAsync(id);

                return Ok(roomId);
            }
            catch (NonExistentException ex)
            {
                return NotFound(ex.GetProblemDetails());
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationProblemDetails(ex.Errors.ToDictionary()));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteBooking([FromServices] DeleteBookingHandler handler, int id)
        {
            try
            {
                await handler.HandleAsync(id);

                return Ok();
            }
            catch (NonExistentException ex)
            {
                return NotFound(ex.GetProblemDetails());
            }
        }
    }
}
