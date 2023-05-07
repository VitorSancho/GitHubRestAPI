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

        public GitHubBusiness(IConfiguration configuration, IGitHubService gitHubService, IGitHubRepository gitHubRepository)
        {
                GitHubService = gitHubService;
            GitHubRepository = gitHubRepository;
            GithubSearchErrorMessage = configuration["Messages:GithubSearchErrorMessage"];
            UpdatedSuccessfullyMessage = configuration["Messages:UpdatedSuccessfullyMessage"];
            MoreDataThanAllowed = configuration["Messages:MoreDataThanAllowed"];

        }

        public async Task<ValidationDTO> UpdateFamousRepositoryFromLanguages(CollectionOfLanguages LanguagesList)
        {
            if (!IsLanguageListValid(LanguagesList))
                return new ValidationDTO(System.Net.HttpStatusCode.BadRequest, isSucesfull: false, message: MoreDataThanAllowed);           

            await GitHubRepository.CleanDatabaseFromLanguages(LanguagesList.languageList);

            LanguagesList.InsertScapeString();

            try
            {
                var repositoriesData = await GetFamousRepositoriesFromLanguagesParallel(LanguagesList);

                var result = await GitHubRepository.SaveFamousRepositories(repositoriesData);

                var validation = new ValidationDTO(System.Net.HttpStatusCode.OK, isSucesfull: true, message: UpdatedSuccessfullyMessage);
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