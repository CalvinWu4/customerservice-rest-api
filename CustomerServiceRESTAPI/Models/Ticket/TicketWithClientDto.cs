using System;
namespace CustomerServiceRESTAPI.Models
{
    public class TicketWithClientDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public ClientDto Client { get; set; }
    }
}
