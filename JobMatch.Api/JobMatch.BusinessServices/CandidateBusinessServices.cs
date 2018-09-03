
using JobMatch.Models;
using Microsoft.Extensions.Configuration;

namespace JobMatch.BusinessServices
{
    public interface ICandidateBusinessServices:IBaseBusinessService<Candidate>
    {
    }

    public class CandidateBusinessServices: BaseBusinessServices<Candidate>, ICandidateBusinessServices
    {
        private readonly IConfiguration _configuration;
        public CandidateBusinessServices(IHttpClientManager httpClientManager, IConfiguration configuration) : base(httpClientManager, configuration)
        {
            _configuration = configuration;
        }

        public override void SetApiEndpointAddress()
        {
            ApiEndPoint = _configuration.GetSection("ApiEndPoint")["Candidates"];
        }
    }
}
