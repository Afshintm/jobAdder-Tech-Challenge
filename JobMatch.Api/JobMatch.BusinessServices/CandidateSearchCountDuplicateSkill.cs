
using JobMatch.Models;

namespace JobMatch.BusinessServices
{
    public class CandidateSearchCountDuplicateSkill : CandidateSearchServices<AddValueForRepeatedSkills,AddValueForRepeatedSkills>
    {
        public CandidateSearchCountDuplicateSkill(IJobBusinessService<AddValueForRepeatedSkills> jobBusinessServices, ICandidateBusinessServices<AddValueForRepeatedSkills> candidateBusinessServices)
            : base(candidateBusinessServices, jobBusinessServices)
        {
        }
    }
}
