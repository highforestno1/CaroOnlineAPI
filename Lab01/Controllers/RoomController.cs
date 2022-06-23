using System;
using Lab01.RequestModels;
using Lab01.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab01.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController: ControllerBase
    {

        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public IActionResult GetRoomList()
        {

            return Ok(_roomService.GetRoomList());
        }

        [HttpPost("create-room-info")]
        public IActionResult CreateRoom([FromBody] CreateRoomRequest request)
        {
            var newRoom = _roomService.CreateRoom(request);
            return Ok(newRoom);
        }
        
        [HttpPost("edit-room-info")]
        public IActionResult EditRoom([FromBody] EditRoomRequest request)
        {
            var newRoom = _roomService.EditRoom(request);
            return Ok(newRoom);
        }
        
        [HttpDelete("delete-room-info/{id}")]
        public IActionResult EditRoom(Guid id)
        {
            var newRoom = _roomService.DeleteRoom(id);
            return Ok(newRoom);
        }
                    
    }
}