using Newtonsoft.Json;
using System.Net.Http.Json;

namespace VehicleManagement.Common.Helpers
{
    public static class HttpClientHelper
    {
        public static async Task<T> GetAsync<T>(string url)
        {
            using (var client = new HttpClient())
            {            
                HttpResponseMessage response = await client.GetAsync($"{url}");
                if (response.IsSuccessStatusCode)
                {
                    var content  = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<T>(content);
                    return result;
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        public static async Task PostAsync(string url, object data)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync($"{url}", data);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception();
                }               
            }
        }

        public static async Task PutAsync(string url, object data)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.PutAsJsonAsync($"{url}", data);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception();
                }
            }
        }

        public static async Task DeleteAsync(string url)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync($"{url}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception();
                }
            }
        }
    }
}
