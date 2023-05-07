using GitHub.Data.Model;
using static GitHub.Data.Model.GitHubRepositoryData;

namespace GitHub.Data
{
    public interface IGitHubRepository
    {
        Task<bool> SaveFamousRepositories(List<RepositoryData> gitHubRepositoriesData);

        Task CleanDatabaseFromLanguages();

        Task<IEnumerable<GitHubRepositoryDetails>> GetCollectedRepositoriesDetails(string? language, int? id);

        Task<IEnumerable<GitHubRepositoryInfo>> GetCollectedRepositories(string? language);
    }
}