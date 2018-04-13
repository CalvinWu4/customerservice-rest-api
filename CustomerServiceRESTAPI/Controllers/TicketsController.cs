using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerServiceRESTAPI.Services;
using CustomerServiceRESTAPI.Entities;
using CustomerServiceRESTAPI.Models;

namespace CustomerServiceRESTAPI.Controllers
{
    [Route("api/[controller]")]
    public class TicketsController : Controller
    {
        TicketRepository _ticketRepository;
        ClientRepository _clientRepository;
        InventoryService _inventoryService;

        public TicketsController(TicketRepository ticketRepository, ClientRepository clientRepository, InventoryService inventoryService)
        {
            _ticketRepository = ticketRepository;
            _clientRepository = clientRepository;
            _inventoryService = inventoryService;
        }

        // GET: api/tickets
        [HttpGet]
        public IActionResult Get()
        {
            var tickets = _ticketRepository.GetAll();
            var result = AutoMapper.Mapper.Map<IEnumerable<TicketWithClientDto>>(tickets);

            return Ok(result);
        }

        // GET api/tickets/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var ticket = _ticketRepository.Get(id);
            if (ticket == null) return NotFound("Could not find ticket");

            var result = AutoMapper.Mapper.Map<TicketWithClientDto>(ticket);
            return Ok(result);
        }

        // POST api/tickets
        [HttpPost]
        public IActionResult Post([FromBody]TicketForCreationDto ticketForCreation, [FromQuery(Name = "clientId")]int clientId)
        {
            var ticket = AutoMapper.Mapper.Map<Ticket>(ticketForCreation);
            var client = _clientRepository.Get(clientId);
            if (client == null) return NotFound("Client not found");

            // Tickets are set to Status=new by default
            ticket.Status = "new";

            client.Tickets.Add(ticket);
            _clientRepository.Update(client);
            if (!_clientRepository.Save()) return BadRequest("Could not create ticket");

            var result = AutoMapper.Mapper.Map<TicketWithClientDto>(ticket);
            return Ok(result);
        }

        // PUT api/tickets/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]TicketForUpdateDto ticketForUpdate)
        {
            var ticket = _ticketRepository.Get(id);
            if (ticket == null) return NotFound("Could not find ticket");

            ticket.Title = ticketForUpdate.Title != null ? ticketForUpdate.Title : ticket.Title;
            ticket.Description = ticketForUpdate.Description != null ? ticketForUpdate.Description : ticket.Description;
            ticket.Status = ticketForUpdate.Status != null ? ticketForUpdate.Status : ticket.Status;

            _ticketRepository.Update(ticket);
            if (!_ticketRepository.Save()) return BadRequest("Could not update ticket");

            return NoContent();
        }

        // DELETE api/tickets/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ticket = _ticketRepository.Get(id);
            if (ticket == null) return NotFound("Could not find ticket");

            _ticketRepository.Delete(ticket);
            if (!_ticketRepository.Save()) return BadRequest("Could not remove ticket");

            return NoContent();
        }
    }
}
