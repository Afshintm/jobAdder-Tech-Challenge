using System.Threading.Tasks;
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
    }
}
