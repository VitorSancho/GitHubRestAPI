using Newtonsoft.Json;
using RestSharp;
using GitHub.Data.Model;
using static GitHub.Data.Model.GitHubRepositoryData;

namespace GitHub.Service
{
    public class GitHubService : IGitHubService
    {
        private string BaseGitHubUrl { get; set; }
        private string SearchRepositoryRoute { get; set; }
        private RestClient HttpClient { get; set; }

        public GitHubService()
        {
            BaseGitHubUrl = "https://api.github.com";
            SearchRepositoryRoute = "search/repositories?q=language:{language}&sort:stars&per_page=5";
            HttpClient = new RestClient(BaseGitHubUrl);
        }

        public async Task<List<RepositoryData>> GetFamousRepositoryFromLanguage(string language)
        {
            Console.WriteLine($"Colletion data of {language} repository");

            var request = new RestRequest(SearchRepositoryRoute.Replace("language",language));
            var requestResult = await HttpClient.GetAsync(request);

            if (requestResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var data = JsonConvert.DeserializeObject<GitHubRepositoryData>(requestResult.Content);                
                return data.items;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}