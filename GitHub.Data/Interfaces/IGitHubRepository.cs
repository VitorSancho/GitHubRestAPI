using GitHub.Data.Model;
using static GitHub.Data.Model.GitHubRepositoryData;

namespace GitHub.Data
{
    public interface IGitHubRepository
    {
        Task<bool> SaveFamousRepositories(List<RepositoryData> gitHubRepositoriesData);

        Task CleanDatabaseFromLanguages(IEnumerable<string> gitHubRepositoriesData);
    }
}