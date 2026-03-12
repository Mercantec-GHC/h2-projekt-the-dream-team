using FluentValidation;
using H2Projekt.API.Extensions;
using H2Projekt.Application.Commands.Bookings;
using H2Projekt.Application.Dto.Bookings;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Handlers.Bookings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace H2Projekt.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookingController : BaseController
    {
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult<List<BookingDto>>> GetAllBookings(CancellationToken cancellationToken, [FromServices] GetAllBookingsHandler handler)
        {
            try
            {
                var bookings = await handler.HandleAsync(cancellationToken);

                return Ok(bookings);
            }
            catch (OperationCanceledException)
            {
                return StatusCode(StatusCodes.Status499ClientClosedRequest);
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult<List<BookingDto>>> GetBookingsByGuestId(CancellationToken cancellationToken, [FromServices] GetBookingsByGuestIdHandler handler, int id)
        {
            if (!WorkContext.IsAdmin() && WorkContext.Id != id)
            {
                return Forbid();
            }

            try
            {
                var bookings = await handler.HandleAsync(id, cancellationToken);

                return Ok(bookings);
            }
            catch (OperationCanceledException)
            {
                return StatusCode(StatusCodes.Status499ClientClosedRequest);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult<BookingOverviewDto>> GetBookingOverview(CancellationToken cancellationToken, [FromServices] GetBookingsOverviewHandler handler)
        {
            try
            {
                var result = await handler.HandleAsync(cancellationToken);

                return Ok(result);
            }
            catch (OperationCanceledException)
            {
                return StatusCode(StatusCodes.Status499ClientClosedRequest);
            }
        }

        // TODO: [Authorize]?
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult<int>> CreateBooking(CancellationToken cancellationToken, [FromServices] CreateBookingHandler handler, [FromBody] CreateBookingCommand createBookingCommand)
        {
            try
            {
                var bookingId = await handler.HandleAsync(createBookingCommand, cancellationToken);

                return Ok(bookingId);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationProblemDetails(ex.Errors.ToDictionary()));
            }
            catch (NonExistentException ex)
            {
                return NotFound(ex.GetProblemDetails());
            }
            catch (OperationCanceledException)
            {
                return StatusCode(StatusCodes.Status499ClientClosedRequest);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult<int>> AssignRoomToBooking(CancellationToken cancellationToken, [FromServices] AssignRoomToBookingHandler handler, int id)
        {
            try
            {
                var roomId = await handler.HandleAsync(id, cancellationToken);

                return Ok(roomId);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationProblemDetails(ex.Errors.ToDictionary()));
            }
            catch (NonExistentException ex)
            {
                return NotFound(ex.GetProblemDetails());
            }
            catch (OperationCanceledException)
            {
                return StatusCode(StatusCodes.Status499ClientClosedRequest);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult> DeleteBooking(CancellationToken cancellationToken, [FromServices] DeleteBookingHandler handler, int id)
        {
            try
            {
                await handler.HandleAsync(WorkContext, id, cancellationToken);

                return Ok();
            }
            catch (ForbidException)
            {
                return Forbid();
            }
            catch (NonExistentException ex)
            {
                return NotFound(ex.GetProblemDetails());
            }
            catch (OperationCanceledException)
            {
                return StatusCode(StatusCodes.Status499ClientClosedRequest);
            }
        }
    }
}
