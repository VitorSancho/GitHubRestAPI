using GitHub.Data.Model;
using static GitHub.Data.Model.GitHubRepositoryData;

namespace GitHub.Service
{
    public interface IGitHubService
    {
        Task<List<RepositoryData>> GetFamousRepositoryFromLanguage(string language);
    }
}