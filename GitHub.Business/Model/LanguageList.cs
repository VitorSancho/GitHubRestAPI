namespace GitHubRestAPI.Business
{
    public class CollectionOfLanguages
    {
        public IEnumerable<string> languageList { get; set; }
    
        public void InsertScapeString()
        {
            foreach(var language in languageList)
            {
                language.Replace("#","%23").Replace("++","%22%22");
            }
        }
    }

    public class Language
    {
        public string languageName { get; set; }
    }
}
