using System;
namespace CustomerServiceRESTAPI.Models
{
    public class CommentForCreationDto
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string Content { get; set; }
        public string CommentedBy { get; set; }
    }
}
