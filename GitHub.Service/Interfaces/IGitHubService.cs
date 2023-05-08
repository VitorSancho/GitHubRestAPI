using GitHub.Data.Model;
using static GitHub.Data.Model.GitHubRepositoryData;

namespace GitHub.Service
{
    /// <summary>
    /// Gives infercace for GitHubAPI use
    /// </summary>
    public interface IGitHubService
    {
        /// <summary>
        /// Uses GitHubAPI to get data from most famous language's repositories.
        /// </summary>
        /// <param name="language">Language that will be search</param>
        Task<List<RepositoryData>> GetFamousRepositoryFromLanguage(string language);
    }
}