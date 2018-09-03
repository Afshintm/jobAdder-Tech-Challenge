using System;
using System.Collections.Generic;
using System.Text;

namespace JobMatch.Models
{
    public class JobSkillWeight
    {
        public Job Job { get; set; }
        public Dictionary<string, int> SkillWeights { get; set; }
    }

}
