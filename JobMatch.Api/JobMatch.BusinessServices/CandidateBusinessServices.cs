
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobMatch.Models;
using Microsoft.Extensions.Configuration;

namespace JobMatch.BusinessServices
{
    public delegate Dictionary<string, int> SkillWeighCalculator(string skills);

    public interface ICandidateBusinessServices<T>:IBaseBusinessService<Candidate>
    {
        IEnumerable<CandidateSkillWeight> GetCandidateSkillWeights();
    }

    public class CandidateBusinessServices<T>: BaseBusinessServices<Candidate>, ICandidateBusinessServices<T> where T: ISkillWeightStrategy , new ()
    {
        public T SkillWeighStrategy { get; set; }
        private readonly IConfiguration _configuration;
        public CandidateBusinessServices(IHttpClientManager httpClientManager, IConfiguration configuration) : base(httpClientManager, configuration)
        {
            _configuration = configuration;
            SkillWeighStrategy = new T();
        }

        public override void SetApiEndpointAddress()
        {
            ApiEndPoint = _configuration.GetSection("ApiEndPoint")["Candidates"];
        }

        public IEnumerable<CandidateSkillWeight> GetCandidateSkillWeights()
        {
            var candidates = Task.Run(async () =>
            {
                var cans = await GetAllAsync();
                return cans.Select(item => new CandidateSkillWeight
                {
                    Candidate = item,
                    SkillWeights = SkillWeighStrategy.CalculateSkillWeights(item.SkillTags)
                });
            });
            return candidates.Result;
        }

        public CandidateSkillWeight GetCandidateSkillWeights(Candidate candidate)
        {
            var candidateSkillWeight = new CandidateSkillWeight()
            {
                Candidate = candidate,
                SkillWeights = SkillWeighStrategy.CalculateSkillWeights(candidate.SkillTags)
            };
            return candidateSkillWeight;
        }

        public CandidateSkillWeight GetCandidateSkillWeights(int candidateId)
        {
            var candidates = Task.Run(async () =>
            {
                var cans = await GetAllAsync();
                return cans;
                
            }).Result;
            var c = candidates.FirstOrDefault(x => x.CandidateId == candidateId);
            if (c == null) return null;
            return new CandidateSkillWeight
            {
                Candidate = c,
                SkillWeights = SkillWeighStrategy.CalculateSkillWeights(c.SkillTags)
            };
        }
    }
}
