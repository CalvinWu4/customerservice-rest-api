using System;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using System.Linq;
using CustomerServiceAPI.Services;
using CustomerServiceRESTAPI.Entities;
using CustomerServiceRESTAPI.Models;
using System.Threading.Tasks;

namespace CustomerServiceRESTAPI.Services
{
    public class HRService : IHRService
    {
        static readonly HttpClient client = new HttpClient();

        public async Task<List<AgentDto>> GetAgentsAsync()
        {
            var serializer = new DataContractJsonSerializer(typeof(List<AgentDto>));
            var agentsStream = client.GetStreamAsync("http://kennuware-1772705765.us-east-1.elb.amazonaws.com/api/employee");
            var agents = serializer.ReadObject(await agentsStream) as List<AgentDto>;
            return agents;
        }

        public AgentDto GetAgent(int id)
        {
            throw new NotImplementedException();
        }

    }
}