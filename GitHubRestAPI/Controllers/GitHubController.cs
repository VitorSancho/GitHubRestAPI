using AutoMapper;
using GitHub.Business;
using GitHub.Business.Model;
using GitHubRestAPI.Business;
using GitHubRestAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace GitHubRestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GitHubController : Controller
    {
        private readonly IGitHubBusiness GitHubBusiness;

        private readonly ILogger<GitHubController> _logger;
        private readonly IMapper Mapper;

        public GitHubController(ILogger<GitHubController> logger, IGitHubBusiness gitHubBusiness, IMapper mapper)
        {
            _logger = logger;
            GitHubBusiness = gitHubBusiness;
            Mapper = mapper;
        }

        [HttpPost]
        [Route("[Action]")]
        public async Task<IActionResult> UpdateFamousRepositoryListFromLanguages([FromBody] LanguagesDTO languageList)
        {
            var languageListMapped =  Mapper.Map<CollectionOfLanguages>(languageList); 

            var result = await GitHubBusiness.UpdateFamousRepositoryFromLanguages(languageListMapped);

            if (result.IsSucesfull)
                return Ok(result);

            return BadRequest(result);    
        }
    }
}