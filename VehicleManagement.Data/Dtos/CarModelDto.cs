using Newtonsoft.Json;

namespace VehicleManagement.Data.Dtos
{
    public class CarModelDto
    {
        [JsonProperty("Make_ID")]
        public int BrandNumber { get; set; }

        [JsonProperty("Make_Name")]
        public string BrandName { get; set; }

        [JsonProperty("Model_ID")]
        public int ModelNumber { get; set; }

        [JsonProperty("Model_Name")]
        public string ModelName { get; set; }
    }
}
