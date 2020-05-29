using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SampleAPI.Contracts;
using SampleAPI.Core;
using SampleAPI.Provider;
using SampleAPI.Tests.Utils;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace SampleAPI.Tests.Functional
{
    public class FunctionalTests
    {
        private WebApplicationFactory<SampleAPI.Startup> _apiUnderTest;

       public FunctionalTests()
        {
            // create a stub for a dependency 
            // and set a fixed result
            var mock = new Mock<IAnotherDatasource>();

            mock
            .Setup(x => x.GetSomeData())
            .ReturnsAsync(new SomeData() { MyProperty = "dummyValue" });

            // create an in-memory web application with in-memory databases
            _apiUnderTest = WebApplicationFactoryBuilder<SampleAPI.Startup>
                .NewTestServer(new Uri("http://localhost:5000"))
                .WithAppSettingsFromEntryPoint("appsettings.json")
                .AddInMemoryDB<MachineContext>(Guid.NewGuid().ToString())
                .ArrangeData<MachineContext>(x =>
                 {
                     x.AddRange(CreateDemoData());
                 })
                .AddTestService<IAnotherDatasource>(x =>
                {
                    x.AddSingleton<IAnotherDatasource>(mock.Object);
                })
                .WithDefaultHostBuilder()
                .Build();
        }

        [Fact]
        public async Task Get_All_Machines()
        {
            var result = await _apiUnderTest
                .CreateClient()
                .GetAsync("http://localhost:5000/api/machines");

            Assert.True(result.IsSuccessStatusCode, $"Unexpected ResultCode {result.StatusCode}");

           var payload = result.Content.ReadAsStringAsync().Result.FromJsonToType<List<Machine>>();

            Assert.Equal(2, payload.Count);    
            // ...
        }
        
        [Fact]
        public async Task Get_ById_ReturnsNone()
        {
            var result = await _apiUnderTest
                .CreateClient()
                .GetAsync($"http://localhost:5000/api/machines/{Guid.NewGuid().ToString()}");

            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task Get_ById_Found()
        {
            var result = await _apiUnderTest
                .CreateClient()
                .GetAsync($"http://localhost:5000/api/machines/0BF5B011-909B-4CD5-B700-4EB1FFC7D436");

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

            var payload = result.Content.ReadAsStringAsync().Result.FromJsonToType<Machine>();
            Assert.NotNull(payload);
            // ...
        }

        private IList<SampleAPI.Contracts.Machine>CreateDemoData()
        {
            return new List<Machine>()
            { new Machine()
            {
                Id = "0BF5B011-909B-4CD5-B700-4EB1FFC7D436",
                MachineName = "A machine",
                FabricSerialnumber = "1234ab",
                InternalSeriesName = "internal1234",
                ManufacturerName = "Microsoft",
                ManufacturingYear = new DateTime(2029, 1, 20),
                PlaceOfManufacturing = "Redmond",
                Serialnumber = "01234",
            },
            new Machine()
            {
                Id = "5AEF4BE9-793B-4ED5-90BD-94B3E478C2A0",
                MachineName = "Another machine",
                FabricSerialnumber = "5678",
                InternalSeriesName = "internal5678",
                ManufacturerName = "Microsoft",
                ManufacturingYear = new DateTime(2029, 1, 21),
                PlaceOfManufacturing = "Redmond",
                Serialnumber = "567890",
            }
            };
        }
    }
}
