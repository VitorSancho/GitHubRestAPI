using GitHub.Data.Model;
using static GitHub.Data.Model.GitHubRepositoryData;

namespace GitHub.Data
{
    public interface IGitHubRepository
    {
        /// <summary>
        /// Saves list of repository data on data base.
        /// </summary>
        /// <param name="gitHubRepositoriesData">Language that will be search</param>
        Task<bool> SaveFamousRepositories(List<RepositoryData> gitHubRepositoriesData);

        /// <summary>
        /// Clean database for a new colletion proccess
        /// </summary>
        Task CleanDatabaseFromLanguages();

        /// <summary>
        /// Get repositories data with it details
        /// </summary>
        /// <param name="language">Language that will be search</param>
        /// <param name="id">Id of specific repository</param>
        Task<IEnumerable<GitHubRepositoryDetails>> GetCollectedRepositoriesDetails(string? language, int? id);

        /// <summary>
        /// Saves list of repository information from data base.
        /// </summary>
        /// <param name="language">Language that will be search</param>
        Task<IEnumerable<GitHubRepositoryInfo>> GetCollectedRepositories(string? language);
    }
}