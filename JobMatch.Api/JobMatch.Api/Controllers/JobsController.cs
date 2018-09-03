using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobMatch.BusinessServices;
using JobMatch.BusinessServices.ViewModels;
using JobMatch.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobMatch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobBusinessService<AddValueForRepeatedSkills> _jobBusinessService;
        private readonly CandidateSearchCountDuplicateSkill _candidateSearchCountDuplicateSkill;
        public JobsController(CandidateSearchCountDuplicateSkill candidateSearchCountDuplicateSkill,  IJobBusinessService<AddValueForRepeatedSkills> jobBusinessService)
        {
            _jobBusinessService = jobBusinessService;
            _candidateSearchCountDuplicateSkill = candidateSearchCountDuplicateSkill;
        }
        [HttpGet]
        public List<JobCandidateViewModel> Get()
        {
            var jobs = _candidateSearchCountDuplicateSkill.ListCandidatesForAllJobs();
            var jobsViewModel = jobs.Select(x => x.ToViewModel());
            return jobsViewModel.ToList();
        }

        [Route("{id:int}")]
        [HttpGet]
        public IActionResult GetJob(int id)
        {
            var job = _candidateSearchCountDuplicateSkill.GetCandidatesForJob(id);
            if (job == null) return NotFound();

            var jobCandidateViewModel = job.ToViewModel();
            return Ok(jobCandidateViewModel);
        }

    }
}