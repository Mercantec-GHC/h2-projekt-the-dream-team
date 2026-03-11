using FluentValidation;
using H2Projekt.API.Extensions;
using H2Projekt.Application.Commands.Rooms;
using H2Projekt.Application.Dto.Rooms;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Handlers.Rooms;
using H2Projekt.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace H2Projekt.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoomController : BaseController
    {
        #region Rooms

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult<List<RoomDto>>> GetAllRooms(CancellationToken cancellationToken, [FromServices] GetAllRoomsHandler handler)
        {
            try
            {
                var rooms = await handler.HandleAsync(cancellationToken);

                return Ok(rooms);
            }
            catch (OperationCanceledException)
            {
                return StatusCode(StatusCodes.Status499ClientClosedRequest);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult<int>> CreateRoom(CancellationToken cancellationToken, [FromServices] CreateRoomHandler handler, [FromBody] CreateRoomCommand createRoomCommand)
        {
            try
            {
                var roomId = await handler.HandleAsync(createRoomCommand, cancellationToken);

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
            catch (DuplicateException ex)
            {
                return Conflict(ex.GetProblemDetails());
            }
            catch (OperationCanceledException)
            {
                return StatusCode(StatusCodes.Status499ClientClosedRequest);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult> UpdateRoom(CancellationToken cancellationToken, [FromServices] UpdateRoomHandler handler, [FromBody] UpdateRoomCommand updateRoomCommand)
        {
            try
            {
                await handler.HandleAsync(updateRoomCommand, cancellationToken);

                return Ok();
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
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult> DeleteRoom(CancellationToken cancellationToken, [FromServices] DeleteRoomHandler handler, int id)
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

        #endregion

        #region Room Types

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult<List<RoomTypeDto>>> GetAllRoomTypes(CancellationToken cancellationToken, [FromServices] GetAllRoomTypesHandler handler)
        {
            try
            {
                var roomTypes = await handler.HandleAsync(cancellationToken);

                return Ok(roomTypes);
            }
            catch (OperationCanceledException)
            {
                return StatusCode(StatusCodes.Status499ClientClosedRequest);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult<AvailableRoomTypesForStayDto>> GetAvailableRoomTypesForStay(CancellationToken cancellationToken, [FromServices] GetAvailableRoomTypesForStayHandler handler, [FromQuery] GetAvailableRoomTypesForStayCommand getAvailableRoomTypesForStayCommand)
        {
            try
            {
                var availableRoomTypesForStay = await handler.HandleAsync(getAvailableRoomTypesForStayCommand, cancellationToken);

                return Ok(availableRoomTypesForStay);
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
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult<int>> CreateRoomType(CancellationToken cancellationToken, [FromServices] CreateRoomTypeHandler handler, [FromBody] CreateRoomTypeCommand createRoomTypeCommand)
        {
            try
            {
                var roomId = await handler.HandleAsync(createRoomTypeCommand, cancellationToken);

                return Ok(roomId);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationProblemDetails(ex.Errors.ToDictionary()));
            }
            catch (DuplicateException ex)
            {
                return Conflict(ex.GetProblemDetails());
            }
            catch (OperationCanceledException)
            {
                return StatusCode(StatusCodes.Status499ClientClosedRequest);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult> UpdateRoomType(CancellationToken cancellationToken, [FromServices] UpdateRoomTypeHandler handler, [FromBody] UpdateRoomTypeCommand updateRoomTypeCommand)
        {
            try
            {
                await handler.HandleAsync(updateRoomTypeCommand, cancellationToken);

                return Ok();
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
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult> DeleteRoomType(CancellationToken cancellationToken, [FromServices] DeleteRoomTypeHandler handler, int id)
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

        #endregion

        #region Room Discounts

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult<List<RoomDiscountDto>>> GetAllRoomDiscounts(CancellationToken cancellationToken, [FromServices] GetAllRoomDiscountsHandler handler)
        {
            try
            {
                var roomDiscounts = await handler.HandleAsync(cancellationToken);

                return Ok(roomDiscounts);
            }
            catch (OperationCanceledException)
            {
                return StatusCode(StatusCodes.Status499ClientClosedRequest);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult<int>> CreateRoomDiscount(CancellationToken cancellationToken, [FromServices] CreateRoomDiscountHandler handler, [FromBody] CreateRoomDiscountCommand createRoomDiscountCommand)
        {
            try
            {
                var roomId = await handler.HandleAsync(createRoomDiscountCommand, cancellationToken);

                return Ok(roomId);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationProblemDetails(ex.Errors.ToDictionary()));
            }
            catch (DuplicateException ex)
            {
                return Conflict(ex.GetProblemDetails());
            }
            catch (OperationCanceledException)
            {
                return StatusCode(StatusCodes.Status499ClientClosedRequest);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult> UpdateRoomDiscount(CancellationToken cancellationToken, [FromServices] UpdateRoomDiscountHandler handler, [FromBody] UpdateRoomDiscountCommand updateRoomDiscountCommand)
        {
            try
            {
                await handler.HandleAsync(updateRoomDiscountCommand, cancellationToken);

                return Ok();
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
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult> DeleteRoomDiscount(CancellationToken cancellationToken, [FromServices] DeleteRoomDiscountHandler handler, int id)
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

        #endregion
    }
}