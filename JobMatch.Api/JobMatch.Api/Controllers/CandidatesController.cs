using System.Collections.Generic;
using System.Linq;
using JobMatch.BusinessServices;
using JobMatch.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobMatch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private ICandidateBusinessServices _candidateBusinessServices;
        public CandidatesController(ICandidateBusinessServices candidateBusinessServices)
        {
            _candidateBusinessServices = candidateBusinessServices;
        }

        [HttpGet]
        public List<Candidate> Get()
        {
            List<Candidate> result = _candidateBusinessServices.GetAllAsync().Result.ToList();
            return result;
        }

    }
}