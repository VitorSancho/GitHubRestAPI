using GitHubBusiness;
using GitHubRestAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace GitHubRestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GitHubController : ControllerBase
    {
        private readonly IGitHubBusiness GitHubBusiness;

        private readonly ILogger<GitHubController> _logger;

        public GitHubController(ILogger<GitHubController> logger, IGitHubBusiness gitHubBusiness)
        {
            _logger = logger;
            GitHubBusiness = gitHubBusiness;
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<ActionResult> UpdateFamousRepositoryListFromLanguages([FromBody] LanguagesDTO languageList)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = GitHubBusiness.UpdateFamousRepositoryFromLanguages(languageList);

            return Ok(languageList);
        }
    }
}