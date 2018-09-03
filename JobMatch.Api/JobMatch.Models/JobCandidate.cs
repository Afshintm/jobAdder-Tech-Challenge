using System;
using System.Collections.Generic;
using System.Text;

namespace JobMatch.Models
{
    public class JobCandidate
    {
        public JobCandidate()
        {
            GoodCandidates = new List<CandidateJobScore>();
        }

        public Job Job { get; set; }
        public List<CandidateJobScore> GoodCandidates { get; set; }
    }

}
