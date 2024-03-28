namespace VehicleManagement.Data.Dtos
{
    public class CarModelApiResultDto
    {
        public int Count { get; set; }
        public string Message { get; set; }
        public string SearchCriteria { get; set; }
        public CarModelDto[] Results { get; set; }
    }
}
