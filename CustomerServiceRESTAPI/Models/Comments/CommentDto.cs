using System;
using System.Collections.Generic;

namespace CustomerServiceRESTAPI.Models
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int ticketId{ get; set; }
        public string content { get; set; }
        public string author { get; set; }
        public string commentedBy { get; set; }
        public string dateCreated { get; set; }
    }
}