using AutoMapper;
using GitHub.Business;
using GitHubRestAPI.Business;
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
        private readonly IMapper Mapper;

        public GitHubController(ILogger<GitHubController> logger, IGitHubBusiness gitHubBusiness, IMapper mapper)
        {
            _logger = logger;
            GitHubBusiness = gitHubBusiness;
            Mapper = mapper;
        }

        [HttpPost]
        [Route("[controller]/teste")]
        public async Task<ActionResult> UpdateFamousRepositoryListFromLanguages([FromBody] LanguagesDTO languageList)
        {
            if (!ModelState.IsValid) return BadRequest();

            var languageListMapped =  Mapper.Map<CollectionOfLanguages>(languageList); 

            var result = await GitHubBusiness.UpdateFamousRepositoryFromLanguages(languageListMapped);

            return Ok(languageList);
        }
    }
}