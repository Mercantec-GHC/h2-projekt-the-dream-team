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
            var guests = await handler.Handle();

            return Ok(guests.Select(guest => new GuestDto(guest)));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GuestDto?>> GetGuestById([FromServices] GetGuestByIdHandler handler, int id)
        {
            try
            {
                var guest = await handler.Handle(id);

                return Ok(new GuestDto(guest));
            }
            catch (NonExistentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
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
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateGuest([FromServices] UpdateGuestHandler handler, [FromBody] UpdateGuestCommand updateGuestCommand)
        {
            try
            {
                await handler.Handle(updateGuestCommand);

                return Ok();
            }
            catch (NonExistentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteGuest([FromServices] DeleteGuestHandler handler, int id)
        {
            try
            {
                await handler.Handle(id);

                return Ok();
            }
            catch (NonExistentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
