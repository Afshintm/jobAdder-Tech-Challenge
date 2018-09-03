using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace JobMatch.BusinessServices
{
    public interface IBaseBusinessService<T>
    {
        IEnumerable<T> GetAll();
        string ApiEndPoint { get; set; }
        Task<IEnumerable<T>> GetAllAsync();
    }

    public abstract class BaseBusinessServices<T> : IBaseBusinessService<T> where T : class
    {

        private IHttpClientManager _httpClientManager;
        private readonly IConfiguration _configuration;
        public string ApiEndPoint { get; set; }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            SetApiEndpointAddress();
            if (string.IsNullOrEmpty(ApiEndPoint))
            {
                throw new ApplicationException($"No endpoint is provided.");
            }
            var result = await _httpClientManager.GetAsync<IEnumerable<T>>(ApiEndPoint);
            return result;
        }

        public BaseBusinessServices(IHttpClientManager httpClientManager, IConfiguration configuration)
        {
            _httpClientManager = httpClientManager;
            _configuration = configuration;
        }

        public abstract void SetApiEndpointAddress();

        public virtual IEnumerable<T> GetAll()
        {
            var result = Task.Run(async () => await GetAllAsync()).Result;
            return result;
        }

    }
}
