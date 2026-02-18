using FluentValidation;
using H2Projekt.Application.Commands.Guests;
using H2Projekt.Application.Dto.Guests;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Handlers.Guests;
using Microsoft.AspNetCore.Mvc;

namespace H2Projekt.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GuestDto>>> GetAllGuests([FromServices] GetAllGuestsHandler handler)
        {
            var guests = await handler.HandleAsync();

            return Ok(guests.Select(guest => new GuestDto(guest)));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GuestDto?>> GetGuestById([FromServices] GetGuestByIdHandler handler, int id)
        {
            try
            {
                var guest = await handler.HandleAsync(id);

                return Ok(new GuestDto(guest));
            }
            catch (NonExistentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> GetGuestIdByEmail(
            [FromQuery] string email,
            [FromServices] GetGuestByEmailHandler handler,
            CancellationToken ct)
        {
            var guest = await handler.HandleAsync(email.Trim().ToLower(), ct);

            if (guest is null)
                return NotFound();
            return Ok(guest.Id);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> CreateGuest([FromServices] CreateGuestHandler handler, [FromBody] CreateGuestCommand createGuestCommand)
        {
            try
            {
                var guestId = await handler.HandleAsync(createGuestCommand);

                return Ok(guestId);
            }
            catch (DuplicateException ex)
            {
                return Conflict(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationProblemDetails(ex.Errors.ToDictionary()));
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateGuest([FromServices] UpdateGuestHandler handler, [FromBody] UpdateGuestCommand updateGuestCommand)
        {
            try
            {
                await handler.HandleAsync(updateGuestCommand);

                return Ok();
            }
            catch (NonExistentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationProblemDetails(ex.Errors.ToDictionary()));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteGuest([FromServices] DeleteGuestHandler handler, int id)
        {
            try
            {
                await handler.HandleAsync(id);

                return Ok();
            }
            catch (NonExistentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
