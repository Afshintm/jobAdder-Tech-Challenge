using System.Collections.Generic;
using System.Linq;
using JobMatch.Models;

namespace JobMatch.BusinessServices
{
    
    public interface ICandidateSearchServices
    {
        IEnumerable<JobCandidate> ListCandidatesForAllJobs();
        JobCandidate GetCandidatesForJob(int jobId);
        JobCandidate PickCandidateForJob(Job job, IEnumerable<CandidateSkillWeight> allCandidates);
    }



    public interface ICandidateSearchServices<TCandidateSkillStrategy, TJobSkillStrategy> :  ICandidateSearchServices
        where TCandidateSkillStrategy : ISkillWeightStrategy
        where TJobSkillStrategy : ISkillWeightStrategy
    {

    }

    public class CandidateSearchServices<TCandidateSkillStrategy, TJobSkillStrategy>: ICandidateSearchServices<TCandidateSkillStrategy, TJobSkillStrategy>
        where TCandidateSkillStrategy : ISkillWeightStrategy
        where TJobSkillStrategy : ISkillWeightStrategy

    {
        private readonly ICandidateJobScoreCalculatorServices<TCandidateSkillStrategy, TJobSkillStrategy> _candidateJobScoreCalculatorServices;
        public CandidateSearchServices(ICandidateBusinessServices<TCandidateSkillStrategy> candidateBusinessServices,
            IJobBusinessService<TJobSkillStrategy> jobBusinessServices, 
            ICandidateJobScoreCalculatorServices<TCandidateSkillStrategy, TJobSkillStrategy> candidateJobScoreCalculatorServices) 
        {
            _candidateJobScoreCalculatorServices = candidateJobScoreCalculatorServices;

        }

        public IEnumerable<JobCandidate> ListCandidatesForAllJobs()
        {
            return _candidateJobScoreCalculatorServices.JobSkillWeights.Select(item => PickCandidateForJob(item.Job, _candidateJobScoreCalculatorServices.CandidateSkillWeights));
        }
        public JobCandidate GetCandidatesForJob(int jobId)
        {
            var allCandidates = _candidateJobScoreCalculatorServices.CandidateSkillWeights;
            var job = _candidateJobScoreCalculatorServices.JobSkillWeights.FirstOrDefault(x => x.Job.JobId == jobId);
            if (job == null) return null;
            var jvm = PickCandidateForJob(job.Job, allCandidates);

            return jvm;
        }

        public JobCandidate PickCandidateForJob(Job job, IEnumerable<CandidateSkillWeight> allCandidates)
        {

            var jvm = new JobCandidate
            {
                Job = job
            };
            foreach (var candidate in allCandidates)
            {
                var candidatejobScore = _candidateJobScoreCalculatorServices.CalculateCandidateScore(candidate.Candidate, job);
                if (candidatejobScore.Score > 0)
                    jvm.GoodCandidates.Add(candidatejobScore);
            }

            jvm.GoodCandidates.Sort((x, y) =>
            {
                if (x.Score > y.Score) return -1;
                if (x.Score < y.Score) return 1;
                return 0;
            });
            return jvm;
        }

    }

}
