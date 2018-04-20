using System;
using System.Collections.Generic;
using System.Text;
using CustomerServiceRESTAPI.Controllers;
using CustomerServiceRESTAPI.Services;
using CustomerServiceRESTAPI.Tests.Mocks;
using Xunit;

namespace CustomerServiceRESTAPI.Tests.Controllers
{
    class ReviewsControllerTests
    {
        public class StartupFixture
        {
            public StartupFixture()
            {
                AutoMapperConfig.Config();
            }
        }
        [CollectionDefinition("StartupFixture collection")]
        public class StartupCollection : ICollectionFixture<StartupFixture>
        {
            // This class has no code, and is never created. Its purpose is simply
            // to be the place to apply [CollectionDefinition] and all the
            // ICollectionFixture<> interfaces.
        }

        [Collection("StartupFixture collection")]
        public class EmptyRepoTests
        {
            private readonly ReviewsController _emptyRepoController;
            private StartupFixture _startupFixture;

            public EmptyRepoTests(StartupFixture startupFixture)
            {
                _startupFixture = startupFixture;
                _emptyRepoController = new ReviewsController(new ReviewRepositoryMock(), new ClientRepositoryMock());
            }

        }


    }
