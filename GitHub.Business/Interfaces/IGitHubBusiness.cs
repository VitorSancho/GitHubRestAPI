using GitHub.Business.Model;
using GitHubRestAPI.Business;

namespace GitHub.Business
{
    public interface IGitHubBusiness
    {
        /// <summary>
        /// Clean database ands save repository data of language that will be search
        /// </summary>
        /// <param name="LanguagesList">List of language that will be searched and saved on database</param>
        Task<ValidationDTO> UpdateFamousRepositoryFromLanguages(CollectionOfLanguages LanguagesList);

        /// <summary>
        /// Get repositories data with it details
        /// </summary>
        /// <param name="language">Language that will be search</param>
        /// <param name="id">Id of specific repository</param>
        Task<ValidationDTO> GetCollectedRepositoriesDetails(string? language,int? id);

        /// <summary>
        /// Get sim data of repositories 
        /// </summary>
        /// <param name="language">Language that will be search</param>
        Task<ValidationDTO> GetCollectedRepositories(string? language);

    }
}