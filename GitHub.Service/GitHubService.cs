using Newtonsoft.Json;
using RestSharp;
using GitHub.Data.Model;
using static GitHub.Data.Model.GitHubRepositoryData;
using Microsoft.Extensions.Configuration;

namespace GitHub.Service
{
    public class GitHubService : IGitHubService
    {
        private string BaseGitHubUrl { get; set; }
        private string SearchRepositoryRoute { get; set; }
        private RestClient HttpClient { get; set; }

        public GitHubService(IConfiguration configuration)
        {
            BaseGitHubUrl = configuration["BaseGitHubUrl"]; ;
            SearchRepositoryRoute = configuration["SearchRepositoryRoute"]; ;
            HttpClient = new RestClient(BaseGitHubUrl);
        }

        /// <summary>
        /// Uses GitHubAPI to get data from most famous language's repositories.
        /// </summary>
        public async Task<List<RepositoryData>> GetFamousRepositoryFromLanguage(string language)
        {
            Console.WriteLine($"Colletion data of {language} repository");

            var request = new RestRequest(SearchRepositoryRoute.Replace("{language}",language));
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