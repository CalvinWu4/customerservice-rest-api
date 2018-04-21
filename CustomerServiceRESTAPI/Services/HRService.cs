using System;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using System.Linq;
using CustomerServiceRESTAPI.Services;
using CustomerServiceRESTAPI.Entities;
using CustomerServiceRESTAPI.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CustomerServiceRESTAPI.Services
{
    public class HRService : IHRService
    {
        static readonly HttpClient client = new HttpClient();

        public async Task<IEnumerable<AgentDto>> GetAgentsAsync()
        {
            var result = await client.GetAsync("http://kennuware-1772705765.us-east-1.elb.amazonaws.com/api/employee");
            if (!result.IsSuccessStatusCode) return null;

            var parsedResponse = JsonConvert.DeserializeObject<AgentListResponse>(await result.Content.ReadAsStringAsync());

            return parsedResponse.Data;
        }

        public async Task<AgentDto> GetAgentAsync(int id)
        {
            var result = await client.GetAsync($"http://kennuware-1772705765.us-east-1.elb.amazonaws.com/api/employee?id={id}");
            if (!result.IsSuccessStatusCode) return null;

            return JsonConvert.DeserializeObject<AgentDto>(await result.Content.ReadAsStringAsync());
        }

        public async Task<AuthHRDto> PostClientAsync(ClientForCreationDto clientForCreation)
        {
            var crendentials = new HRCustomerCrendential()
            {
                username = clientForCreation.Email,
                password = clientForCreation.Password
            };

            var result = await client.PostAsync("http://kennuware-1772705765.us-east-1.elb.amazonaws.com/auth/create", new StringContent(JsonConvert.SerializeObject(crendentials)));
            if (!result.IsSuccessStatusCode) return null;

            var parsedResponse = JsonConvert.DeserializeObject<AuthHRDto>(await result.Content.ReadAsStringAsync());

            return parsedResponse;
        }
    }

    class AgentListResponse
    {
        public IEnumerable<AgentDto> Data { get; set; }
    }

    class HRCustomerCrendential
    {
        public string username { get; set; }
        public string password { get; set; }
        public string type { get; set; } = "customer";
    }
}