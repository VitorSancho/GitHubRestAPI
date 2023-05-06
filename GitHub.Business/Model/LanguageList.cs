namespace GitHubRestAPI.Model
{
    public class CollectionOfLanguages
    {
        public IEnumerable<Language> languageList;
    
        public void AvaliateNames()
        {
            foreach(var language in languageList)
            {
                language.languageName.Replace().Replace();
            }
        }
    }

    public class Language
    {
        public string languageName;
    }
}
