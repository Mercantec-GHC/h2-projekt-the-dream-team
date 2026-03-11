using H2Projekt.API.Extensions;
using H2Projekt.Application.Commands.Authentication;
using H2Projekt.Application.Dto.Authentication;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Handlers.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;

namespace H2Projekt.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult<AuthResponseDto>> Login(CancellationToken cancellationToken, [FromServices] LoginHandler handler, [FromBody] LoginCommand loginCommand)
        {
            try
            {
                var result = await handler.HandleAsync(loginCommand, cancellationToken);

                return Ok(result);
            }
            catch (InvalidCredentialException ex)
            {
                return Unauthorized(ex.GetProblemDetails());
            }
            catch (OperationCanceledException)
            {
                return StatusCode(StatusCodes.Status499ClientClosedRequest);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult<AuthResponseDto>> Refresh(CancellationToken cancellationToken, [FromServices] RefreshHandler handler, [FromBody] string refreshToken)
        {
            try
            {
                var guestId = await handler.HandleAsync(refreshToken, cancellationToken);

                return Ok(guestId);
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

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult<int>> Register(CancellationToken cancellationToken, [FromServices] RegisterHandler handler, [FromBody] RegisterCommand registerCommand)
        {
            try
            {
                var guestId = await handler.HandleAsync(registerCommand, cancellationToken);

                return Ok(guestId);
            }
            catch (InvalidCredentialException ex)
            {
                return Unauthorized(ex.GetProblemDetails());
            }
            catch (OperationCanceledException)
            {
                return StatusCode(StatusCodes.Status499ClientClosedRequest);
            }
        }
    }
}
