using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using VehicleManagement.API.Controllers;
using VehicleManagement.Data.Dtos;
using VehicleManagement.Services;
using Xunit.Abstractions;

namespace VehicleManagement.UnitTest
{
    public class VehicleManagementTest
    {
        IConfiguration _configuration;
        ICarModelService _carModelService;
        CarModelApiController _carModelApiController;
        private readonly ITestOutputHelper _output;


        public VehicleManagementTest(ITestOutputHelper output)
        {
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _carModelService = new CarModelService(_configuration);
            _carModelApiController = new CarModelApiController(_carModelService);
            _output = output;
        }

        [Fact]
        public async Task GetLincoln2015CarModels()
        {
            int modelYear = 2015;
            string make = "Lincoln";

            var result = await _carModelApiController.Get(modelYear, make);

            Assert.IsType<CarModelResultDto>(result);

            var list = result.Models;
            Assert.True(list != null && list.Any());

            _output.WriteLine(JsonConvert.SerializeObject(result));
        }
    }
}