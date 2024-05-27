using HotelManagmentAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;
using HotelManagmentAPI.Dto;
using Microsoft.Extensions.Logging;

namespace HotelManagmentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IHotelRepository _roomRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RoomController> _logger;

        public RoomController(IHotelRepository roomRepository, IMapper mapper, ILogger<RoomController> logger)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Room
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RoomDto>))]
        public IActionResult GetRooms()
        {
            _logger.LogInformation("Getting all rooms");
            var rooms = _mapper.Map<List<RoomDto>>(_roomRepository.GetRooms());
            return Ok(rooms);
        }

        // GET: api/Room/id/5
        [HttpGet("id/{roomID:int}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RoomDto>))]
        [ProducesResponseType(404)]
        public IActionResult GetRoomById(int roomID)
        {
            _logger.LogInformation($"Getting room with ID {roomID}");

            if (!_roomRepository.RoomExists(roomID))
            {
                _logger.LogWarning($"Room with ID {roomID} not found");
                return NotFound();
            }

            var room = _mapper.Map<RoomDto>(_roomRepository.GetRoom(roomID));
            return Ok(room);
        }

        // GET: api/Room/type/Deluxe
        [HttpGet("type/{roomType}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RoomDto>))]
        [ProducesResponseType(404)]
        public IActionResult GetRoomByType(string roomType)
        {
            _logger.LogInformation($"Getting room with type {roomType}");

            var room = _mapper.Map<RoomDto>(_roomRepository.GetRoomByType(roomType));

            if (room == null)
            {
                _logger.LogWarning($"Room with type {roomType} not found");
                return NotFound();
            }

            return Ok(room);
        }

    }
}
