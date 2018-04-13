using System;
namespace CustomerServiceRESTAPI.Models
{
    public class ReviewDtoForUpdate
    {
        public int AgentId { get; set; }
        public string Content { get; set; }
        public string DateCreated { get; set; }
    }
}
