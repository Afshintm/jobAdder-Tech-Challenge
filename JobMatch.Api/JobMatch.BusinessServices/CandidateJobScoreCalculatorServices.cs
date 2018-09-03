using System.Collections.Generic;
using System.Linq;
using JobMatch.Models;

namespace JobMatch.BusinessServices
{
    public interface ICandidateJobScoreCalculatorServices 
    {
        CandidateJobScore CalculateCandidateScore(Candidate candidate, Job job);
        IEnumerable<CandidateSkillWeight> CandidateSkillWeights { get; set; }
        IEnumerable<JobSkillWeight> JobSkillWeights { get; set; }

    }

    public interface
        ICandidateJobScoreCalculatorServices<TCandidateSkillStrategy, TJobSkillStrategy> : ICandidateJobScoreCalculatorServices 
        where TCandidateSkillStrategy : ISkillWeightStrategy
        where TJobSkillStrategy : ISkillWeightStrategy
    {

    }

    public class CandidateJobScoreCalculatorServices<TCandidateSkillStrategy,TJobSkillStrategy> : ICandidateJobScoreCalculatorServices<TCandidateSkillStrategy, TJobSkillStrategy> 
        where TCandidateSkillStrategy : ISkillWeightStrategy
        where TJobSkillStrategy : ISkillWeightStrategy
    {
        private readonly IJobBusinessService<TJobSkillStrategy> _jobBusinessService;
        private readonly ICandidateBusinessServices<TCandidateSkillStrategy> _candidateBusinessServices;


        public IEnumerable<CandidateSkillWeight> CandidateSkillWeights { get; set; }
        public IEnumerable<JobSkillWeight> JobSkillWeights { get; set; }

        public CandidateJobScoreCalculatorServices(ICandidateBusinessServices<TCandidateSkillStrategy> candidateBusinessServices,
            IJobBusinessService<TJobSkillStrategy> jobBusinessServices)
        {
            _jobBusinessService = jobBusinessServices;
            _candidateBusinessServices = candidateBusinessServices;
            CandidateSkillWeights = _candidateBusinessServices.GetCandidateSkillWeights();
            JobSkillWeights = _jobBusinessService.GetJobSkillWeights();
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
