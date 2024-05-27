using AutoMapper;
using HotelManagmentAPI.Dto;
using HotelManagmentAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HotelManagmentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientsRepository _clientsRepository;
        private readonly IMapper _mapper;

        public ClientController(IClientsRepository clientsRepository, IMapper mapper)
        {
            _clientsRepository = clientsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetClients()
        {
            var clients = _clientsRepository.GetClients();
            var clientsDto = _mapper.Map<List<ClientDto>>(clients);
            return Ok(clientsDto);
        }

        [HttpGet("{clientID}")]
        public IActionResult GetClient(int clientID)
        {
            var client = _clientsRepository.GetClient(clientID);
            if (client == null)
            {
                return NotFound();
            }
            var clientDto = _mapper.Map<ClientDto>(client);
            return Ok(clientDto);
        }

        [HttpGet("clientName/{clientName}")]
        public IActionResult GetClientByName(string clientName)
        {
            var client = _clientsRepository.GetClientByName(clientName);
            if (client == null)
            {
                return NotFound();
            }
            var clientDto = _mapper.Map<ClientDto>(client);
            return Ok(clientDto);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateClient([FromBody] ClientDto clientDto)
        {
            if (clientDto == null)
                return BadRequest();

            if (_clientsRepository.GetClientByName(clientDto.FirstName) != null)
            {
                ModelState.AddModelError("", "Client already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var client = _mapper.Map<Client>(clientDto);

            if (!_clientsRepository.CreateClient(client))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            var createdDto = _mapper.Map<ClientDto>(client);
            return CreatedAtAction(nameof(GetClient), new { clientID = createdDto.ClientID }, createdDto);
        }


        [HttpPut("{clientID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateClient(int clientID, [FromBody] ClientDto updatedClient)
        {
           if(updatedClient == null)
                return BadRequest(ModelState);

           if(clientID != updatedClient.ClientID)
                return BadRequest(ModelState);

           if(!_clientsRepository.ClientExists(clientID))
                return NotFound();

           if(!ModelState.IsValid)
                return BadRequest(ModelState);

           var clientMap = _mapper.Map<Client>(updatedClient);

            if (!_clientsRepository.UpdateClient(clientMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating the client");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{clientID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteClient(int clientID)
        {
            if(!_clientsRepository.ClientExists(clientID))
                return NotFound();

            var categoryToDelete = _clientsRepository.GetClient(clientID);

            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            if (!_clientsRepository.DeleteClient(clientID))
            {
                ModelState.AddModelError("", "Something went wrong while deleting the client");
                return StatusCode(500, ModelState);
            }

            return NoContent();


        }
    }
}
