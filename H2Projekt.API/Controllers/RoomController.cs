using FluentValidation;
using H2Projekt.Application.Commands.Rooms;
using H2Projekt.Application.Dto.Rooms;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Handlers.Rooms;
using H2Projekt.Application.Interfaces;
using H2Projekt.Application.Rooms.Handlers;
using H2Projekt.Domain;
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
        public async Task<ActionResult<List<RoomDto>>> GetAllRooms([FromServices] GetAllRoomsHandler handler)
        {
            var rooms = await handler.Handle();

            return Ok(rooms.Select(room => new RoomDto(room)));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoomDto?>> GetRoomByNumber([FromServices] GetRoomByNumberHandler handler, string number)
        {
            try
            {
                var room = await handler.Handle(number);

                return Ok(new RoomDto(room));
            }
            catch (NonExistentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<int>> CreateRoom([FromServices] CreateRoomHandler handler, [FromBody] CreateRoomCommand createRoomCommand)
        {
            try
            {
                var roomId = await handler.Handle(createRoomCommand);

                return Ok(roomId);
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
        public async Task<ActionResult> UpdateRoom([FromServices] UpdateRoomHandler handler, [FromBody] UpdateRoomCommand updateRoomCommand)
        {
            try
            {
                await handler.Handle(updateRoomCommand);

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

        [HttpDelete("{number}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteRoom([FromServices] DeleteRoomHandler handler, string number)
        {
            try
            {
                await handler.Handle(number);

                return Ok();
            }
            catch (NonExistentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        #endregion

        #region Room Types

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<RoomTypeDto>>> GetAllRoomTypes([FromServices] GetAllRoomTypesHandler handler)
        {
            var roomTypes = await handler.Handle();

            return Ok(roomTypes.Select(roomType => new RoomTypeDto(roomType)));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<int>> CreateRoomType([FromServices] CreateRoomTypeHandler handler, [FromBody] CreateRoomTypeCommand createRoomTypeCommand)
        {
            try
            {
                var roomId = await handler.Handle(createRoomTypeCommand);

                return Ok(roomId);
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
        public async Task<ActionResult> UpdateRoomType([FromServices] UpdateRoomTypeHandler handler, [FromBody] UpdateRoomTypeCommand updateRoomTypeCommand)
        {
            try
            {
                await handler.Handle(updateRoomTypeCommand);

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
        public async Task<ActionResult> DeleteRoomType([FromServices] DeleteRoomTypeHandler handler, int id)
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

        #endregion
    }
}