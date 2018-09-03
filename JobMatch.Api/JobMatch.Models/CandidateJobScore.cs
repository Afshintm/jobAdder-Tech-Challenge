using System;
using System.Collections.Generic;
using System.Text;

namespace JobMatch.Models
{
 
    public class CandidateJobScore
    {
        public CandidateJobScore(Candidate candidate, Job job)
        {
            Candidate = candidate;
            Job = job;
        }

        public Candidate Candidate { get; }
        public Job Job { get; }
        public int Score { get; set; }
    }
}
