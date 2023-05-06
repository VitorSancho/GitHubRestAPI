using GitHub.Business.Model;
using GitHubRestAPI.Business;
using GitHubRestAPI.Model;

namespace GitHub.Business
{
    public interface IGitHubBusiness
    {
        Task<ValidationDTO> UpdateFamousRepositoryFromLanguages(CollectionOfLanguages LanguagesList);
    }
}