using System;
using System.Collections.Generic;
using System.Text;

namespace JobMatch.Models
{
    public class CandidateSkillWeight
    {
        public Candidate Candidate { get; set; }
        public Dictionary<string, int> SkillWeights { get; set; }
    }
}
