using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerServiceRESTAPI.Entities;
using CustomerServiceRESTAPI.Models;

namespace CustomerServiceAPI.Services
{
    public interface IHRService
    {
        Task<List<AgentDto>> GetAgentsAsync();
        AgentDto GetAgent(int id);
    }
}
