using GitHub.Business.Model;
using GitHubRestAPI.Business;

namespace GitHub.Business
{
    public interface IGitHubBusiness
    {
        Task<ValidationDTO> UpdateFamousRepositoryFromLanguages(CollectionOfLanguages LanguagesList);

        Task<ValidationDTO> GetCollectedRepositoriesDetails(string? language,int? id);

        Task<ValidationDTO> GetCollectedRepositories(string? language);

    }
}