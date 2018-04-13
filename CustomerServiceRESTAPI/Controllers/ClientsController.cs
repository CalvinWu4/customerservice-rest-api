using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerServiceRESTAPI.Services;
using CustomerServiceRESTAPI.Models;
using CustomerServiceRESTAPI.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerServiceRESTAPI.Controllers
{
    [Route("api/[controller]")]
    public class ClientsController : Controller
    {
        ClientRepository _clientRepository;

        public ClientsController(ClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var clients = _clientRepository.GetAll();
            var result = AutoMapper.Mapper.Map<IEnumerable<ClientWithTicketsAndReviewsDto>>(clients);

            return Ok(result);
        }

        // GET api/clients/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var client = _clientRepository.Get(id);
            if (client == null) return NotFound("Client not found");

            var result = AutoMapper.Mapper.Map<ClientWithTicketsAndReviewsDto>(client);
            return Ok(result);
        }

        // POST api/clients
        [HttpPost]
        public IActionResult Post([FromBody]ClientForCreationDto clientForCreation)
        {
            var client = AutoMapper.Mapper.Map<Client>(clientForCreation);

            _clientRepository.Add(client);
            if (!_clientRepository.Save()) return BadRequest("Could not create client");

            var result = AutoMapper.Mapper.Map<ClientWithTicketsAndReviewsDto>(client);
            return Ok(result);
        }

        // PUT api/clients/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ClientForUpdateDto clientForUpdate)
        {
            var client = _clientRepository.Get(id);
            if (client == null) return NotFound("Client not found");

            client.FirstName = clientForUpdate.FirstName != null ? clientForUpdate.FirstName : client.FirstName;
            client.LastName = clientForUpdate.LastName != null ? clientForUpdate.LastName : client.LastName;
            client.Email = clientForUpdate.Email != null ? clientForUpdate.Email : client.Email;

            _clientRepository.Update(client);
            if (!_clientRepository.Save()) return BadRequest("Could not update client");

            return NoContent();
        }

        // DELETE api/clients/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var client = _clientRepository.Get(id);
            if (client == null) return NotFound("Client not found");

            _clientRepository.Delete(client);
            if (!_clientRepository.Save()) return BadRequest("Could not delete client");

            return NotFound();
        }
    }
}
