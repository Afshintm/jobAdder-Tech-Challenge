using System.Net.Http;
using System.Threading.Tasks;

namespace JobMatch.BusinessServices
{
    public interface IHttpClientManager
    {
        Task<T> GetAsync<T>(string path) where T : class;
    }

    public class HttpClientManager : IHttpClientManager
    {
        private HttpClient client;
        public HttpClientManager()
        {
            client = new HttpClient();
        }

        public async Task<T> GetAsync<T>(string path) where T : class
        {
            T obj = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                obj = await response.Content.ReadAsAsync<T>();
            }
            return obj;
        }
        
    }
}
