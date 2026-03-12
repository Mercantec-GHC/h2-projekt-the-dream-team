using FluentValidation;
using H2Projekt.API.Extensions;
using H2Projekt.Application.Commands.Guests;
using H2Projekt.Application.Dto.Bookings;
using H2Projekt.Application.Dto.Guests;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Handlers.Bookings;
using H2Projekt.Application.Handlers.Guests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace H2Projekt.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GuestController : BaseController
    {
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult<List<GuestDto>>> GetAllGuests(CancellationToken cancellationToken, [FromServices] GetAllGuestsHandler handler)
        {
            try
            {
                var guests = await handler.HandleAsync(cancellationToken);

                return Ok(guests);
            }
            catch (OperationCanceledException)
            {
                return StatusCode(StatusCodes.Status499ClientClosedRequest);
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult<GuestDto?>> GetGuestById(CancellationToken cancellationToken, [FromServices] GetGuestByIdHandler handler, int id)
        {
            if (!WorkContext.IsAdmin() && WorkContext.Id != id)
            {
                return Forbid();
            }

            try
            {
                var guest = await handler.HandleAsync(id, cancellationToken);

                return Ok(guest);
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

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult<GuestDto>> GetGuestByEmail(CancellationToken cancellationToken, [FromServices] GetGuestByEmailHandler handler, string email)
        {
            if (!WorkContext.IsAdmin() && WorkContext.Email != email)
            {
                return Forbid();
            }

            try
            {
                var guest = await handler.HandleAsync(email, cancellationToken);

                return Ok(guest);
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

        [Authorize]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult> UpdateGuest(CancellationToken cancellationToken, [FromServices] UpdateGuestHandler handler, [FromBody] UpdateGuestCommand updateGuestCommand)
        {
            if (!WorkContext.IsAdmin() && WorkContext.Id != updateGuestCommand.Id)
            {
                return Forbid();
            }

            try
            {
                await handler.HandleAsync(updateGuestCommand, cancellationToken);

                return Ok();
            }
            catch (NonExistentException ex)
            {
                return NotFound(ex.GetProblemDetails());
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationProblemDetails(ex.Errors.ToDictionary()));
            }
            catch (OperationCanceledException)
            {
                return StatusCode(StatusCodes.Status499ClientClosedRequest);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult> DeleteGuest(CancellationToken cancellationToken, [FromServices] DeleteGuestHandler handler, int id)
        {
            try
            {
                await handler.HandleAsync(id, cancellationToken);

                return Ok();
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
