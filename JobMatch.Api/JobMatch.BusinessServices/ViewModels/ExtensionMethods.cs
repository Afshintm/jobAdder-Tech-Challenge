using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JobMatch.Models;

namespace JobMatch.BusinessServices.ViewModels
{
    public static class ExtensionMethods
    {
        public static JobCandidateViewModel ToViewModel(this JobCandidate jobCandidate)
        {
            if (jobCandidate == null) return null;
            return new JobCandidateViewModel()
            {
                JobId = jobCandidate.Job.JobId,
                Name = jobCandidate.Job.Name,
                Company = jobCandidate.Job.Company,
                GoodCandidates = jobCandidate.GoodCandidates.Select(x => x.ToViewModel()).ToList()
            };

        }

        public static CandidateJobScoreViewModel ToViewModel(this CandidateJobScore candidateJobScore)
        {
            if (candidateJobScore == null)
                return null;
            return new CandidateJobScoreViewModel
            {
                CandidateId = candidateJobScore.Candidate.CandidateId,
                Name = candidateJobScore.Candidate.Name,
                Score = candidateJobScore.Score,
                SkillTags = candidateJobScore.Candidate.SkillTags
            };

        }
    }
}
