using FluentValidation;
using H2Projekt.Application.Commands;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Handlers;
using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;
using Microsoft.AspNetCore.Mvc;

namespace H2Projekt.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;

        public RoomController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        #region Rooms

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Room>>> GetAllRooms()
        {
            var rooms = await _roomRepository.GetAllRoomsAsync();

            return Ok(rooms);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Room?>> GetRoomByNumber(string number)
        {
            var room = await _roomRepository.GetRoomByNumberAsync(number);

            if (room is null)
            {
                NotFound($"Room with number {number} not found.");
            }

            return Ok(room);
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
        public async Task<ActionResult<List<RoomType>>> GetAllRoomTypes()
        {
            var roomTypes = await _roomRepository.GetAllRoomTypesAsync();

            return Ok(roomTypes);
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