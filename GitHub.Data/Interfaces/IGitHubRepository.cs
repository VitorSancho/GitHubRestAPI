using GitHubRestAPI.Model;
using static GitHubRestAPI.Model.GitHubRepositoryData;

namespace GitHub.Data
{
    public interface IGitHubRepository
    {
        Task SaveFamousRepositories(List<RepositoryData> gitHubRepositoriesData);
    }
}