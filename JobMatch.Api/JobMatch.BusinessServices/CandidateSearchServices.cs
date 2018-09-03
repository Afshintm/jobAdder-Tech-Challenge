using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JobMatch.Models;

namespace JobMatch.BusinessServices
{
    
    public interface ICandidateSearchServices
    {
        IEnumerable<JobCandidate> ListCandidatesForAllJobs();
        JobCandidate GetCandidatesForJob(int jobId);
        JobCandidate PickCandidateForJob(Job job, IEnumerable<CandidateSkillWeight> allCandidates);
    }



    public interface ICandidateSearchServices<TCandidateSkillStrategy, TJobSkillStrategy> : ICandidateJobScoreCalculatorServices<TCandidateSkillStrategy, TJobSkillStrategy>, ICandidateSearchServices
        where TCandidateSkillStrategy : ISkillWeightStrategy
        where TJobSkillStrategy : ISkillWeightStrategy
    {

    }

    public class CandidateSearchServices<TCandidateSkillStrategy, TJobSkillStrategy>: CandidateJobScoreCalculatorServices<TCandidateSkillStrategy, TJobSkillStrategy> , ICandidateSearchServices<TCandidateSkillStrategy, TJobSkillStrategy>
        where TCandidateSkillStrategy : ISkillWeightStrategy
        where TJobSkillStrategy : ISkillWeightStrategy

    {
        public CandidateSearchServices(ICandidateBusinessServices<TCandidateSkillStrategy> candidateBusinessServices,
            IJobBusinessService<TJobSkillStrategy> jobBusinessServices) :base(candidateBusinessServices, jobBusinessServices)
        {
        }

        public IEnumerable<JobCandidate> ListCandidatesForAllJobs()
        {
            return JobSkillWeights.Select(item => PickCandidateForJob(item.Job, CandidateSkillWeights));
        }
        public JobCandidate GetCandidatesForJob(int jobId)
        {
            var allCandidates = CandidateSkillWeights;
            var job = JobSkillWeights.FirstOrDefault(x => x.Job.JobId == jobId);
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
                var candidatejobScore = CalculateCandidateScore(candidate.Candidate, job);
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
