using System;
using System.Collections.Generic;

namespace CustomerServiceRESTAPI.Models
{
    public class CommentWithTicketsDto
    {
        public int Id { get; set; }
        public int ticketId { get; set; }
        public string content { get; set; }
        public string author { get; set; }
        public string commentedBy { get; set; }
        public string dateCreated { get; set; }

        public ICollection<TicketDto> Tickets { get; set; } = new List<TicketDto>();
    }
}
