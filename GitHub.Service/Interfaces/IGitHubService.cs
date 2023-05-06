using GitHubRestAPI.Model;
using static GitHubRestAPI.Model.GitHubRepositoryData;

namespace GitHub.Service
{
    public interface IGitHubService
    {
        Task<List<RepositoryData>> GetFamousRepositoryFromLanguage(string language);
    }
}