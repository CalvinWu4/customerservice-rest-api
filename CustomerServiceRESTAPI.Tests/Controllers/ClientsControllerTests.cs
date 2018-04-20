﻿using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using CustomerServiceRESTAPI.Controllers;
using CustomerServiceRESTAPI.Models;
using CustomerServiceRESTAPI.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
namespace CustomerServiceRESTAPI.Tests.Controllers
{
    [Collection("StartupFixture collection")]
    public class ClientsControllerTests
    {
        public void Create_Client()
        {
            var controller = new ClientsController(new ClientRepositoryMock());
            var clientForCreation = new ClientForCreationDto()
            {
                FirstName = "Steven",
                LastName = "BOI",
                Email = "Hello",
                Password = "mypasswkrd",
                Address = new Address()
                {
                    Line1 = "45 some st",
                    Line2 = "apt 2",
                    City = "Roc",
                    State = "MA",
                    Zipcode = "02142",
                    Country = "US"
                }
            };


            var result = controller.Post(clientForCreation);

            var okResult = result.Should().BeOfType<CreatedAtRouteResult>().Subject;
            var client = okResult.Value.Should().BeAssignableTo<ClientWithTicketsAndReviewsDto>().Subject;

            client.Address.City.Should().Be(clientForCreation.Address.City);
        }

        [Fact]
        public void Get_Client()
        {
            var controller = new ClientsController(new ClientRepositoryMock());

            var result = controller.Get();
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var clients = okResult.Value.Should().BeAssignableTo<IEnumerable<ClientWithTicketsAndReviewsDto>>().Subject;
            clients.Count().Should().Be(1);
        }
    }
}
