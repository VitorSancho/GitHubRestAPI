using GitHub.Business.Model;
using GitHub.Data;
using GitHub.Service;
using GitHubRestAPI.Business;
using GitHubRestAPI.Model;
using static GitHubRestAPI.Model.GitHubRepositoryData;

namespace GitHub.Business
{
    public class GitHubBusiness : IGitHubBusiness
    {

        private readonly IGitHubService GitHubService;
        private readonly IGitHubRepository GitHubRepository;
        private readonly string GithubSearchErrorMessage;

        public GitHubBusiness(IGitHubService gitHubService, IGitHubRepository gitHubRepository)
        {
            GitHubService = gitHubService;
            GitHubRepository = gitHubRepository;
            GithubSearchErrorMessage = "";
        }

        public async Task<ValidationDTO> UpdateFamousRepositoryFromLanguages(CollectionOfLanguages LanguagesList)
        {
            //if (!IsLanguageListValid(LanguagesList))
            //    return null;   
            
            LanguagesList.InsertScapeString();

            try
            {
                var repositoriesData = await GetFamousRepositoriesFromLanguages(LanguagesList);

                await GitHubRepository.SaveFamousRepositories(repositoriesData);
            }
            catch
            {
                var validation = new ValidationDTO(System.Net.HttpStatusCode.BadGateway, isSucesfull: false, message: GithubSearchErrorMessage);
            
            }
            return null;
        }

        private bool IsLanguageListValid(CollectionOfLanguages LanguageList)
        {
            return LanguageList.languageList.Count() == 5;
        }
        
        private async Task<List<RepositoryData>> GetFamousRepositoriesFromLanguages(CollectionOfLanguages LanguagesList)
        {
            var getRespositoriesDataTask = LanguagesList.languageList.Select(language => GitHubService.GetFamousRepositoryFromLanguage(language));
            var RespositoriesData = await Task.WhenAll(getRespositoriesDataTask);

            return RespositoriesData.SelectMany(r => r).ToList();
        }
    }
}