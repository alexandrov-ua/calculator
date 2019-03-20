using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Calculator.IntegrationTests.Tools
{
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> PostJson<T>(this HttpClient client, string url, T obj)
        {
            return await client.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json"));
        }

        public static async Task<HttpResponseMessage> ThrowIfNotSuccess(this Task<HttpResponseMessage> task)
        {
            var response = await task;
            response.EnsureSuccessStatusCode();
            return response;
        }

        public static async Task<T> ToModel<T>(this Task<HttpResponseMessage> task)
        {
            var response = await task;
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }
    }
}