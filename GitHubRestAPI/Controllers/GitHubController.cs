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

            return ReturnResponse(result);
        }

        [HttpGet]
        [Route("[Action]")]
        public async Task<IActionResult> GetCollectedRepositories(string? language=null)
        {
            var result = await GitHubBusiness.GetCollectedRepositories(language);

            return ReturnResponse(result);
        }

        [HttpGet]
        [Route("[Action]")]
        public async Task<IActionResult> GetCollectedRepositoriesDetails(string? language = null, int? id=0)
        {
            var result = await GitHubBusiness.GetCollectedRepositoriesDetails(language, id);

            return ReturnResponse(result);
        }

        private IActionResult ReturnResponse(ValidationDTO result)
        {
            if (result.IsSucesfull)
                return Ok(result);

            return BadRequest(result);
        }
    }
}