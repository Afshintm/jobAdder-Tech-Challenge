using JobMatch.Models;
using Microsoft.Extensions.Configuration;

namespace JobMatch.BusinessServices
{
    public interface IJobBusinessService:IBaseBusinessService<Job>
    {
        
    }

    public class JobBusinessServices: BaseBusinessServices<Job>, IJobBusinessService
    {
        private IConfiguration _configuration;

        public JobBusinessServices(IHttpClientManager httpClientManager, IConfiguration configuration) : base(httpClientManager, configuration)
        {
            _configuration = configuration;
        }

        public override void SetApiEndpointAddress()
        {
            ApiEndPoint = _configuration.GetSection("ApiEndpoint")["Jobs"];
        }

    }
}
