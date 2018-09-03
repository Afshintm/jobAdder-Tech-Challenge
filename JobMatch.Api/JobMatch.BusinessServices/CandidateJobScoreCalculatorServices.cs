using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobMatch.Models;

namespace JobMatch.BusinessServices
{
    public interface ICandidateJobScoreCalculatorServices<T>
    {
        CandidateJobScore CalculateCandidateScore(Candidate candidate, Job job);
        IEnumerable<CandidateSkillWeight> CandidateSkillWeights { get; set; }
        IEnumerable<JobSkillWeight> JobSkillWeights { get; set; }

    }



    public class CandidateJobScoreCalculatorServices<T> : ICandidateJobScoreCalculatorServices<T> where T : ISkillWeightStrategy, new()
    {
        private readonly IJobBusinessService _jobBusinessService;
        private readonly ICandidateBusinessServices _candidateBusinessServices;
        private readonly ISkillWeightStrategy _skillsCalculationStrategy;


        public IEnumerable<CandidateSkillWeight> CandidateSkillWeights { get; set; }
        public IEnumerable<JobSkillWeight> JobSkillWeights { get; set; }

        public CandidateJobScoreCalculatorServices(
            IJobBusinessService jobBusinessServices,
            ICandidateBusinessServices candidateBusinessServices, ISkillWeightStrategy skillWeightStrategy)
        {
            _jobBusinessService = jobBusinessServices;
            _candidateBusinessServices = candidateBusinessServices;
            _skillsCalculationStrategy = skillWeightStrategy;
            WeightCandidateSkills();
        }


        public void WeightCandidateSkills()
        {
            var candidates = Task.Run(async () =>
            {
                var cans = await _candidateBusinessServices.GetAllAsync();
                return cans.Select(item => new CandidateSkillWeight
                {
                    Candidate = item,
                    SkillWeights = _skillsCalculationStrategy.CalculateSkillWeights(item.SkillTags)
                });
            });
            var jobs = Task.Run(async () =>
            {
                var j = await _jobBusinessService.GetAllAsync();
                return j.Select(item => new JobSkillWeight
                {
                    Job = item,
                    SkillWeights = _skillsCalculationStrategy.CalculateSkillWeights(item.Skills)
                });
            });

            Task.WaitAll(candidates, jobs);
            CandidateSkillWeights = candidates.Result;
            JobSkillWeights = jobs.Result;
        }

        public CandidateJobScore CalculateCandidateScore(Candidate candidate, Job job)
        {
            var result = new CandidateJobScore(candidate, job);

            var weightedSkillcandidate =
                CandidateSkillWeights.FirstOrDefault(x => x.Candidate.CandidateId == candidate.CandidateId);
            var weightedSkillJob = JobSkillWeights.FirstOrDefault(x => x.Job.JobId == job.JobId);

            if (weightedSkillcandidate != null && weightedSkillJob != null)
            {

                foreach (var requireSkill in weightedSkillJob.SkillWeights.Keys)
                {
                    if (weightedSkillcandidate.SkillWeights.ContainsKey(requireSkill))
                    {
                        result.Score += weightedSkillJob.SkillWeights[requireSkill] * weightedSkillcandidate.SkillWeights[requireSkill];
                    }
                }
            }
            return result;
        }
    }

}
