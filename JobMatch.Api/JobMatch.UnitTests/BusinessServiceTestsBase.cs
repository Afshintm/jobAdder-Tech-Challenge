
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobMatch.BusinessServices;
using JobMatch.Models;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace JobMatch.UnitTests
{
    [TestFixture]
    public class BusinessServiceTestsBase
    {

        public IJobBusinessService<AddValueForRepeatedSkills> JobBusinessService { get; set; }
        public ICandidateBusinessServices<AddValueForRepeatedSkills> CandidateBusinessServices { get; set; }

        public ICandidateJobScoreCalculatorServices<AddValueForRepeatedSkills, AddValueForRepeatedSkills> CandidateJobScoreCalculatorServicesCountDuplicate { get; set; }

        public Mock<IHttpClientManager> HttpClientManagerMock { get; set; }
        public Mock<IConfiguration> ConfigurationMock { get; set; }
        public const string FakeApiEndpoint = "FakeApiEndpoin";

        public List<Job> FakeJobs { get; set; }
        public List<Candidate> FakeCandidates { get; set; }

        [SetUp]
        public void Setup()
        {
            InitFakes();
            ConfigurationMock = new Mock<IConfiguration>();
            ConfigurationMock.Setup(x => x.GetSection(It.IsAny<string>())[It.IsAny<string>()]).Returns(FakeApiEndpoint);

            HttpClientManagerMock = new Mock<IHttpClientManager>(MockBehavior.Default);
            HttpClientManagerMock.Setup(x => x.GetAsync<IEnumerable<Job>>(It.IsAny<string>())).Returns(() => Task.FromResult(FakeJobs.AsEnumerable()));
            HttpClientManagerMock.Setup(x => x.GetAsync<IEnumerable<Candidate>>(It.IsAny<string>())).Returns(() => Task.FromResult(FakeCandidates.AsEnumerable()));

            CandidateBusinessServices = new CandidateBusinessServices<AddValueForRepeatedSkills>(HttpClientManagerMock.Object, ConfigurationMock.Object);
            JobBusinessService = new JobBusinessServices<AddValueForRepeatedSkills>(HttpClientManagerMock.Object, ConfigurationMock.Object);

            CandidateJobScoreCalculatorServicesCountDuplicate = new CandidateJobScoreCalculatorServices<AddValueForRepeatedSkills, AddValueForRepeatedSkills>(CandidateBusinessServices,JobBusinessService);
        }

        public void InitFakes()
        {
            FakeCandidates = new List<Candidate>
                {
                    new Candidate{CandidateId = 1, Name = "Candidate1", SkillTags = "x-code,java,x-code,c,details"},
                    new Candidate{CandidateId = 2, Name = "Candidate2", SkillTags = "management,requiremnets,communications,iOS"},
                    new Candidate{CandidateId = 3, Name = "Candidate3", SkillTags = "fast-typing,ms-office,illustrator,outlook,powerpoint"}
                };

            FakeJobs = new List<Job>()
                {
                    new Job() {JobId = 1, Name = "Job1", Company = "company1", Skills = "details,c,requiremnets,java,unix"},
                    new Job() {JobId = 2, Name = "Job2", Company = "company2", Skills = "x-code,java,iOS,java,details,fast-typing"},
                    new Job() {JobId = 3, Name = "Job3", Company = "company3", Skills = "powerpoint, photoshop, teamwork, detail, service"},
                    new Job() {JobId = 4, Name = "Job4", Company = "company4", Skills = "ms-office,illustrator,outlook,details,c,requiremnets"},
                };
        }

    }
}
