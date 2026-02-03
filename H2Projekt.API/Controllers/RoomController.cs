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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Room>>> GetAllRooms()
        {
            var rooms = await _roomRepository.GetAllRoomsAsync();

            return Ok(rooms);
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
            catch (RoomDuplicateException ex)
            {
                return Conflict(ex.Message);
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
            catch (RoomNonExistentException ex)
            {
                return NotFound(ex.Message);
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
            catch (RoomNonExistentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}