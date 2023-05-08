using GitHub.Business.Model;
using GitHub.Data;
using GitHub.Data.Model;
using GitHub.Service;
using GitHubRestAPI.Business;
using Microsoft.Extensions.Configuration;
using System.Collections.Concurrent;
using System.Configuration;
using System.Data.SqlClient;
using static GitHub.Data.Model.GitHubRepositoryData;

namespace GitHub.Business
{
    public class GitHubBusiness : IGitHubBusiness
    {

        private readonly IGitHubService GitHubService;
        private readonly IGitHubRepository GitHubRepository;
        private readonly string GithubSearchErrorMessage;
        private readonly string UpdatedSuccessfullyMessage;
        private readonly string MoreDataThanAllowed;
        private readonly string GetExecuted;

        public GitHubBusiness(IConfiguration configuration, IGitHubService gitHubService, IGitHubRepository gitHubRepository)
        {
            GitHubService = gitHubService;
            GitHubRepository = gitHubRepository;
            GithubSearchErrorMessage = configuration["Messages:GithubSearchErrorMessage"];
            UpdatedSuccessfullyMessage = configuration["Messages:UpdatedSuccessfullyMessage"];
            MoreDataThanAllowed = configuration["Messages:MoreDataThanAllowed"];
            GetExecuted = configuration["Messages:GetExecuted"];

        }

        /// <summary>
        /// Clean database ands save repository data of language that will be search
        /// </summary>
        /// <param name="LanguagesList">List of language that will be searched and saved on database</param>
        public async Task<ValidationDTO> UpdateFamousRepositoryFromLanguages(CollectionOfLanguages LanguagesList)
        {
            if (!IsLanguageListValid(LanguagesList))
                return new ValidationDTO(System.Net.HttpStatusCode.BadRequest, isSucesfull: false, message: MoreDataThanAllowed);           

            await GitHubRepository.CleanDatabaseFromLanguages();

            LanguagesList.InsertScapeString();

            try
            {
                var repositoriesData = await GetFamousRepositoriesFromLanguagesParallel(LanguagesList);

                var result = await GitHubRepository.SaveFamousRepositories(repositoriesData);

                var validation = new ValidationDTO(System.Net.HttpStatusCode.Created, isSucesfull: true, message: UpdatedSuccessfullyMessage);
                return validation;
            }
            catch
            {
                var validation = new ValidationDTO(System.Net.HttpStatusCode.BadGateway, isSucesfull: false, message: GithubSearchErrorMessage);
                return validation;
            }
        }

        /// <summary>
        /// Get repositories data with it details
        /// </summary>
        /// <param name="language">Language that will be search</param>
        /// <param name="id">Id of specific repository</param>
        public async Task<ValidationDTO> GetCollectedRepositoriesDetails(string? language, int? id)
        {
            try 
            {
                var result = await GitHubRepository.GetCollectedRepositoriesDetails(language, id);

                var validation = new ValidationDTO(System.Net.HttpStatusCode.OK, isSucesfull: false, message: GetExecuted, result);
                return validation;
            }
            catch
            {
                var validation = new ValidationDTO(System.Net.HttpStatusCode.BadGateway, isSucesfull: false, message: GithubSearchErrorMessage);
                return validation;
            }
        }

        /// <summary>
        /// Get sim data of repositories 
        /// </summary>
        /// <param name="language">Language that will be search</param>
        public async Task<ValidationDTO> GetCollectedRepositories(string language)
        {
            try
            {
                var result = await GitHubRepository.GetCollectedRepositories(language);
                var validation = new ValidationDTO(System.Net.HttpStatusCode.OK, isSucesfull: true, message: GetExecuted, result);
                return validation;
            }
            catch
            {
                var validation = new ValidationDTO(System.Net.HttpStatusCode.BadGateway, isSucesfull: false, message: GithubSearchErrorMessage);
                return validation;
            }
        }

        private bool IsLanguageListValid(CollectionOfLanguages LanguageList)
        {
            return LanguageList.languageList.Count() <= 5;
        }

        private async Task<List<RepositoryData>> GetFamousRepositoriesFromLanguagesParallel(CollectionOfLanguages LanguagesList)
        {
            var RespositoriesData = new ConcurrentBag<RepositoryData>();

            ParallelOptions parallelOptions = new()
            { MaxDegreeOfParallelism = 5 };

            Parallel.ForEach(LanguagesList.languageList, language =>
            {
                var data = GitHubService.GetFamousRepositoryFromLanguage(language).Result;

                Parallel.ForEach(data, repositoryData =>
                {
                    RespositoriesData.Add(repositoryData);
                });
            });

            return RespositoriesData.ToList();
        }
    }
}