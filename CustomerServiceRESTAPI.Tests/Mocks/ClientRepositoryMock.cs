using System;
using System.Linq;
using CustomerServiceRESTAPI.Services;
using CustomerServiceRESTAPI.Entities;
using System.Collections.Generic;

namespace CustomerServiceRESTAPI.Tests.Mocks
{
    public class ClientRepositoryMock : IDBRepository<Client>
    {
        List<Client> _clients = new List<Client>();

        public void Add(Client client)
        {
            _clients.Add(client);
        }

        public Client Get(int id)
        {
            return _clients.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Client> GetAll()
        {
            return _clients;
        }

        public void Update(Client client)
        {
            var clientIndex = _clients.FindIndex(c => c.Id == client.Id);
            if (clientIndex == -1) return;

            _clients[clientIndex] = client;
        }

        public void Delete(Client client)
        {
            var clientIndex = _clients.FindIndex(c => c.Id == client.Id);
            if (clientIndex == -1) return;

            _clients.RemoveAt(clientIndex);
        }

        public bool Save()
        {
            return true;
        }
    }
}
