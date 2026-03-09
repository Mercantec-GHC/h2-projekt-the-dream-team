using FluentValidation;
using H2Projekt.Application.Commands.Rooms;
using H2Projekt.Application.Dto.Rooms;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Handlers.Rooms;
using Microsoft.AspNetCore.Mvc;

namespace H2Projekt.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        #region Rooms

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

        [HttpGet("{number}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult<RoomDto?>> GetRoomByNumber(CancellationToken cancellationToken, [FromServices] GetRoomByNumberHandler handler, string number)
        {
            try
            {
                var room = await handler.HandleAsync(number, cancellationToken);

                return Ok(room);
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult<int>> CreateRoom(CancellationToken cancellationToken, [FromServices] CreateRoomHandler handler, [FromBody] CreateRoomCommand createRoomCommand)
        {
            try
            {
                var roomId = await handler.HandleAsync(createRoomCommand, cancellationToken);

                return Ok(roomId);
            }
            catch (DuplicateException ex)
            {
                return Conflict(ex.GetProblemDetails());
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

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult> UpdateRoom(CancellationToken cancellationToken, [FromServices] UpdateRoomHandler handler, [FromBody] UpdateRoomCommand updateRoomCommand)
        {
            try
            {
                await handler.HandleAsync(updateRoomCommand, cancellationToken);

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

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult<int>> CreateRoomType(CancellationToken cancellationToken, [FromServices] CreateRoomTypeHandler handler, [FromBody] CreateRoomTypeCommand createRoomTypeCommand)
        {
            try
            {
                var roomId = await handler.HandleAsync(createRoomTypeCommand, cancellationToken);

                return Ok(roomId);
            }
            catch (DuplicateException ex)
            {
                return Conflict(ex.GetProblemDetails());
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

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult> UpdateRoomType(CancellationToken cancellationToken, [FromServices] UpdateRoomTypeHandler handler, [FromBody] UpdateRoomTypeCommand updateRoomTypeCommand)
        {
            try
            {
                await handler.HandleAsync(updateRoomTypeCommand, cancellationToken);

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

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult<int>> CreateRoomDiscount(CancellationToken cancellationToken, [FromServices] CreateRoomDiscountHandler handler, [FromBody] CreateRoomDiscountCommand createRoomDiscountCommand)
        {
            try
            {
                var roomId = await handler.HandleAsync(createRoomDiscountCommand, cancellationToken);

                return Ok(roomId);
            }
            catch (DuplicateException ex)
            {
                return Conflict(ex.GetProblemDetails());
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

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        public async Task<ActionResult> UpdateRoomDiscount(CancellationToken cancellationToken, [FromServices] UpdateRoomDiscountHandler handler, [FromBody] UpdateRoomDiscountCommand updateRoomDiscountCommand)
        {
            try
            {
                await handler.HandleAsync(updateRoomDiscountCommand, cancellationToken);

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

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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