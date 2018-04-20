﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using CustomerServiceRESTAPI.Services;
using CustomerServiceRESTAPI.Entities;

namespace CustomerServiceRESTAPI.Tests.Mocks
{
    class ReviewRepositoryMock : IDBRepository<Review>
    {
        List<Review> _reviews = new List<Review>();

        public void Add(Review entity)
        {
            _reviews.Add(entity);
        }

        public void Delete(Review entity)
        {
            _reviews.Remove(entity);
        }

        public Review Get(int id)
        {
            return _reviews.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Review> GetAll()
        {
            return _reviews;
        }

        public bool Save()
        {
            return true;
        }

        public void Update(Review entity)
        {
            int index = _reviews.FindIndex(r => r.Id == entity.Id);
            if (index != -1)
            {
                _reviews[index] = entity;
            }
        }

        public IEnumerable<Review> GetAllByAgentId(int agentId)
        {
            return _reviews.Where(r => r.AgentId == agentId);
        }

    }
}
