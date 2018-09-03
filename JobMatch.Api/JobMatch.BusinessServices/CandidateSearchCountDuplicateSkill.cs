
using JobMatch.Models;

namespace JobMatch.BusinessServices
{
    public class CandidateSearchCountDuplicateSkill :  CandidateSearchServices<AddValueForRepeatedSkills>
    {
        public CandidateSearchCountDuplicateSkill(IJobBusinessService jobBusinessServices, ICandidateBusinessServices candidateBusinessServices, ISkillWeightStrategy skillWeightStrategy)
            : base(jobBusinessServices, candidateBusinessServices, skillWeightStrategy)
        {
        }
    }
}
