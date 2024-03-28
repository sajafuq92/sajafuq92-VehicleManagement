using Microsoft.AspNetCore.Mvc;
using VehicleManagement.Data.Dtos;
using VehicleManagement.Services;

namespace VehicleManagement.API.Controllers
{
    [Route("api/models")]
    [ApiController]
    public class CarModelApiController : ControllerBase
    {
        public readonly ICarModelService _carModelService;

        public CarModelApiController(ICarModelService carModelService)
        {
            _carModelService = carModelService;
        }

        [HttpGet("{modelYear}/{make}")]
        public async Task<CarModelResultDto> Get(int modelYear, string make)
        {
            try
            {
                if (modelYear <= 0)
                {
                    throw new Exception("modelYear is Empty!");
                }

                if (string.IsNullOrEmpty(make))
                {
                    throw new Exception("make is Empty!");
                }

                var result = await _carModelService.GetModelsForMakeIdYear(modelYear, make);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
