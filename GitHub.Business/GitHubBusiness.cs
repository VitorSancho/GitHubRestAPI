using GitHub.Business.Model;
using GitHubRestAPI.Model;

namespace GitHubBusiness
{
    public class GitHubBusiness : IGitHubBusiness
    {
        public Task<ValidationDTO> UpdateFamousRepositoruFromLanguages(CollectionOfLanguages LanguagesList)
        {
            if (IsLanguageListValid(LanguagesList)) 
               LanguagesList.AvaliateNames();

            try
            {
                _GitHubService.GetFamousRepositoryFromLanguage();

                await _GitHubRepository.SaveFamousRepositoryFromLanguage();
            }
            catch 
            {
            
            
            }
            return null;
        }

        private bool IsLanguageListValid(CollectionOfLanguages LanguageList)
        {
            return LanguageList.languageList.Count() == 5;
        }     
    }
}