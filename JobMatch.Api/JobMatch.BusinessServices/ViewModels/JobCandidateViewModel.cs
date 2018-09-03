using System;
using System.Collections.Generic;
using System.Text;

namespace JobMatch.BusinessServices.ViewModels
{
    public class JobCandidateViewModel
    {
        public int JobId { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public List<CandidateJobScoreViewModel> GoodCandidates { get; set; }
    }
}
