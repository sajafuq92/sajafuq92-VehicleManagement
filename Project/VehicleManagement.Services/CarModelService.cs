using Microsoft.Extensions.Configuration;
using VehicleManagement.Common.Helpers;
using VehicleManagement.Data.Dtos;

namespace VehicleManagement.Services
{
    public class CarModelService : ICarModelService
    {
        private readonly IConfiguration _configuration;
   
        public CarModelService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<CarModelResultDto> GetModelsForMakeIdYear(int modelYear, string make)
        {
            string carModelApiUrl = _configuration["AppSettings:CarModelApiUrl"];
            string brandFilePath = $"{Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().IndexOf("VehicleManagement."))}{_configuration["AppSettings:BrandFilePath"]}";

          
            var brands = ExcelReadHelper.ReadExcelFile<BrandDto>(brandFilePath);
            if (brands != null && brands.Count > 0)
            {
                var brand = brands.FirstOrDefault(x => !string.IsNullOrEmpty(x.BrandName) && x.BrandName.Trim().ToLower() == make.Trim().ToLower());
                if (brand != null)
                {
                    var brandId = brand.BrandNumber;
                    carModelApiUrl = string.Format(carModelApiUrl, brandId, modelYear);
                    var carModels = await HttpClientHelper.GetAsync<CarModelApiResultDto>(carModelApiUrl);
                    if (carModels.Count > 0)
                    {
                        var result = new CarModelResultDto();
                        result.Models = carModels.Results.Select(x => x.ModelName).ToList();
                        return result;
                    }
                }
            }

            return new CarModelResultDto();
        }
    }
}
