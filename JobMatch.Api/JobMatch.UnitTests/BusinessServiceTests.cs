using System.Linq;
using System.Threading.Tasks;
using JobMatch.BusinessServices;
using JobMatch.Models;
using NUnit.Framework;

namespace JobMatch.UnitTests
{
    [TestFixture()]
    public class BusinessServiceTests: BusinessServiceTestsBase
    {
        [Test]
        public void JobBusinessServiceShouldReturnJobList()
        {
            var jobs = Task.Run(async ()=> await JobBusinessService.GetAllAsync()).Result ;
            Assert.IsNotNull(jobs);
        }
        [Test]
        public void CandidateBusinessServiceShouldReturnJobList()
        {
            var candidates = Task.Run(async () => await CandidateBusinessServices.GetAllAsync()).Result;
            Assert.IsNotNull(candidates);
        }
        [Test]
        public void CandidateSearchService_Should_Count_Duplicate_Skills()
        {
            var candidateSearchService = new CandidateSearchServices<AddValueForRepeatedSkills, AddValueForRepeatedSkills>(CandidateBusinessServices, JobBusinessService);
            var candidateSkillWeights = candidateSearchService.CandidateSkillWeights.ToList();
            Assert.NotNull(candidateSkillWeights);
        }
        [Test]
        public void CandidateSearchService_Should_Ignore_Duplicate_Skills()
        {
            var jobBusinessService =
                new JobBusinessServices<IgnoreRepeatedSkills>(HttpClientManagerMock.Object, ConfigurationMock.Object);
            var candidateBusinessService = new CandidateBusinessServices<IgnoreRepeatedSkills>(HttpClientManagerMock.Object, ConfigurationMock.Object);
            var candidateSearchService = new CandidateSearchServices<IgnoreRepeatedSkills, IgnoreRepeatedSkills>(
                candidateBusinessService, jobBusinessService);
            var candidateSkillWeights = candidateSearchService.CandidateSkillWeights.ToList();
            Assert.NotNull(candidateSkillWeights);
        }
    }
}
