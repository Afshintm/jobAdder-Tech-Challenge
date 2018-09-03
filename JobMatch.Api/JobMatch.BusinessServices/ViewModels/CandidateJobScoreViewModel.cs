using System;
using System.Collections.Generic;
using System.Text;

namespace JobMatch.BusinessServices.ViewModels
{
    public class CandidateJobScoreViewModel
    {
        public int CandidateId { get; set; }
        public string CandidateName { get; set; }
        public string SkillTags { get; set; }
        public int Score { get; set; }

    }
}
