using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobMatch.Models;
using Microsoft.Extensions.Configuration;

namespace JobMatch.BusinessServices
{
    public interface IJobBusinessService<TJobSkillStrategy>:IBaseBusinessService<Job>
    {
        IEnumerable<JobSkillWeight> GetJobSkillWeights();
    }

    public class JobBusinessServices<TJobSkillStrategy>: BaseBusinessServices<Job>, IJobBusinessService<TJobSkillStrategy> where TJobSkillStrategy: ISkillWeightStrategy, new()
    {
        private TJobSkillStrategy SkillWeighStrategy { get; set; }
        private IConfiguration _configuration;

        public JobBusinessServices(IHttpClientManager httpClientManager, IConfiguration configuration) : base(httpClientManager, configuration)
        {
            _configuration = configuration;
            SkillWeighStrategy = new TJobSkillStrategy();
        }

        public override void SetApiEndpointAddress()
        {
            ApiEndPoint = _configuration.GetSection("ApiEndpoint")["Jobs"];
        }

        public IEnumerable<JobSkillWeight> GetJobSkillWeights()
        {
            var candidates = Task.Run(async () =>
            {
                var cans = await GetAllAsync();
                return cans.Select(item => new JobSkillWeight
                {
                    Job = item,
                    SkillWeights = SkillWeighStrategy.CalculateSkillWeights(item.Skills)
                });
            });
            return candidates.Result;
        }

        public JobSkillWeight GetJobSkillWeights(Job job)
        {
            var candidateSkillWeight = new JobSkillWeight()
            {
                Job = job,
                SkillWeights = SkillWeighStrategy.CalculateSkillWeights(job.Skills)
            };
            return candidateSkillWeight;
        }

        public JobSkillWeight GetJobSkillWeights(int jobId)
        {
            var jobs = Task.Run(async () =>
            {
                var jsJobs = await GetAllAsync();
                return jsJobs;

            }).Result;
            var c = jobs.FirstOrDefault(x => x.JobId == jobId);
            if (c == null) return null;
            return new JobSkillWeight
            {
                Job = c,
                SkillWeights = SkillWeighStrategy.CalculateSkillWeights(c.Skills)
            };
        }

    }
}
