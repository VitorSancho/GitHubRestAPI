using AutoMapper;
using GitHub.Business;
using GitHub.Business.Model;
using GitHubRestAPI.Business;
using GitHubRestAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace GitHubRestAPI.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
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

        /// <summary>
        /// Update Database with famous repositories of specific programming languages.
        /// </summary>
        /// <remarks>
        /// That endpoint deletes all data of previos execution and insert new repository data of programming languages that was passed on payload.
        /// 
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///         "languageList": [
        ///                 "C#","python","Java","Javascript","ruby"
        ///                         ]
        ///     }
        ///
        /// You can pass 5 os less languages, you musn't pass more than 5 languages.
        /// </remarks>
        /// <response code="201">Database was sucessfully updated</response>
        /// <response code="400">Something went wrong on process of get data on GitHub API or you passed more than 5 languages in payload</response>
        [ProducesResponseType(typeof(ValidationDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationDTO), StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Route("[Action]")]
        public async Task<IActionResult> UpdateFamousRepositoryListFromLanguages([FromBody] LanguagesDTO languageList)
        {
            var languageListMapped = Mapper.Map<CollectionOfLanguages>(languageList);

            var result = await GitHubBusiness.UpdateFamousRepositoryFromLanguages(languageListMapped);

            return ReturnResponse(result);
        }

        /// <summary>
        /// Get list of repositories' information collected previously 
        /// </summary>
        /// <remarks>
        /// Some repository information: id, name, full name, language, owner login and data of creation.
        /// 
        /// 
        /// Sample request:
        ///
        ///     GET
        ///        language :  python
        ///        id : 13425
        /// </remarks>
        /// <response code="200">Return List of repositories' information</response>
        /// <response code="400">Something went wrong on request</response>
        [ProducesResponseType(typeof(ValidationDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationDTO), StatusCodes.Status400BadRequest)]
        [HttpGet]
        [Route("[Action]")]
        public async Task<IActionResult> GetCollectedRepositories(string? language = null)
        {
            var result = await GitHubBusiness.GetCollectedRepositories(language);

            return ReturnResponse(result);
        }

        /// <summary>
        /// Get list of repositories' details collected previously
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     GET
        ///        language :  python
        ///        id : 13425
        ///        
        /// </remarks>
        /// <response code="200">Return List of repositories' details</response>
        /// <response code="400">Something went wrong on request</response>    
        [ProducesResponseType(typeof(ValidationDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationDTO), StatusCodes.Status400BadRequest)]
        [HttpGet]
        [Route("[Action]")]
        public async Task<IActionResult> GetCollectedRepositoriesDetails(string? language = null, int? id = 0)
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