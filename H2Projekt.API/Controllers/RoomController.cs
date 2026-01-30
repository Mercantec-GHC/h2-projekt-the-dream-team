using H2Projekt.Application.Commands;
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> CreateRoom([FromServices] CreateRoomHandler handler, [FromBody] CreateRoomCommand createRoomCommand)
        {
            try
            {
                var roomId = await handler.Handle(createRoomCommand);

                return Ok(roomId);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
