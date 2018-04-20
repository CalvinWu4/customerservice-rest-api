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
        public IActionResult Get([FromQuery(Name = "productSerialNumber")]string productSerialNumber, [FromQuery(Name = "clientId")]int clientId = -1)
        {
            var tickets = _ticketRepository.GetAll();

            if (clientId != -1) tickets = tickets.Where(t => t.ClientId == clientId);
            if (productSerialNumber != null) tickets = tickets.Where(t => t.ProductSerialNumber == productSerialNumber);

            var result = AutoMapper.Mapper.Map<IEnumerable<TicketWithClientDto>>(tickets);

            return Ok(result);
        }

        // GET api/tickets/5
        [HttpGet("{id}", Name = "GetTicket")]
        public IActionResult Get(int id)
        {
            var ticket = _ticketRepository.Get(id);
            if (ticket == null) return NotFound("Could not find ticket");

            var result = AutoMapper.Mapper.Map<TicketWithClientDto>(ticket);
            return Ok(result);
        }

        // POST api/tickets
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TicketForCreationDto ticketForCreation, [FromQuery(Name = "clientId")]int clientId)
        {
            var ticket = AutoMapper.Mapper.Map<Ticket>(ticketForCreation);
            // Look up the client by id
            var client = _clientRepository.Get(clientId);
            // Ensure client exists
            if (client == null) return NotFound("Client not found");
            // Look up the product by serial number
            var productDetails = await _inventoryService.GetProductAsync(ticketForCreation.ProductSerialNumber);
            // Ensure the product exists
            if (productDetails == null)
            {
                return NotFound("Serial number not found");
            }
            // Ensure the product has been sold
            if (!(productDetails.Status == "sold"))
            {
                return BadRequest("This device has not been sold");
            }
            // Create the ticket
            // Tickets are set to Status=new by default
            ticket.Status = "new";
            // Tickets are associated with the product they were created for
            ticket.ProductSerialNumber = productDetails.SerialNumber;

            client.Tickets.Add(ticket);
            _clientRepository.Update(client);
            if (!_clientRepository.Save()) return BadRequest("Could not create ticket");

            var result = AutoMapper.Mapper.Map<TicketWithClientDto>(ticket);
            return CreatedAtRoute("GetTicket", new { id = ticket.Id }, result);
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

        // POST api/tickets/5/replace
        [Route("{id}/replace")]
        [HttpPost]
        public async Task<IActionResult> Replace(int id)
        {
            // Look up the ticket
            var ticket = _ticketRepository.Get(id);
            if (ticket == null) return NotFound("Could not find ticket");
            // Place the device replacement request
            if ((await new SalesService().RequestReplacementDevice(ticket)))
            {
                // Request succeeded
                return Ok();
            }
            // Request failed
            return BadRequest("Sales' endpoint is still down");
        }
    }
}
