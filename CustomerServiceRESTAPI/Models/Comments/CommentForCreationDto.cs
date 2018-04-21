using System;
namespace CustomerServiceRESTAPI.Models
{
    public class CommentForCreationDto
    {
        public int Id { get; set; }
        public int ticketId { get; set; }
        public string content { get; set; }
        public int agentId { get; set; }
        public string commentedBy { get; set; }
        public string dateCreated { get; set; }
    }
}
