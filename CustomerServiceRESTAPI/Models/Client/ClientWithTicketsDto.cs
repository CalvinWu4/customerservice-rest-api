using System;
using System.Collections.Generic;

namespace CustomerServiceRESTAPI.Models
{
    public class ClientWithTicketsDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public ICollection<TicketDto> Tickets { get; set; } = new List<TicketDto>();
    }
}
