using GitHub.Business.Model;
using GitHubRestAPI.Model;

namespace GitHubBusiness
{
    public interface IGitHubBusiness
    {
        Task<ValidationDTO> UpdateFamousRepositoruFromLanguages(CollectionOfLanguages LanguagesList);
    }
}