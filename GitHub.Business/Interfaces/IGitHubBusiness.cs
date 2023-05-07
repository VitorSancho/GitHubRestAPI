using GitHub.Business.Model;
using GitHubRestAPI.Business;

namespace GitHub.Business
{
    public interface IGitHubBusiness
    {
        Task<ValidationDTO> UpdateFamousRepositoryFromLanguages(CollectionOfLanguages LanguagesList);
    }
}