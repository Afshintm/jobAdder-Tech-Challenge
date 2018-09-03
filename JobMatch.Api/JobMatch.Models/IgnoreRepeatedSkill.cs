using System;
using System.Collections.Generic;
using System.Text;

namespace JobMatch.Models
{
    public class IgnoreRepeatedSkills : ISkillWeightStrategy
    {
        public Dictionary<string, int> CalculateSkillWeights(string skills)
        {
            var skillsArray = skills.Split(',');
            var length = skillsArray.Length;
            var skillWeight = new Dictionary<string, int>();
            foreach (var item in skillsArray)
            {
                var key = item.ToLower().Trim();
                if (skillWeight.ContainsKey(key))
                {
                    length--;
                }
                else
                {
                    skillWeight.Add(key, length--);
                }
            }
            return skillWeight;
        }
    }
}
