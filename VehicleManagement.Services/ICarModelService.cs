
using VehicleManagement.Data.Dtos;

namespace VehicleManagement.Services
{
    public interface ICarModelService
    {
        public Task<CarModelResultDto> GetModelsForMakeIdYear(int modelYear, string make);
    }
}
