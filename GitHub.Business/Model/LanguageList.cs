namespace GitHubRestAPI.Business
{
    public class CollectionOfLanguages
    {
        public List<string> languageList { get; set; }

        public void InsertScapeString()
        {
            languageList = languageList.Select(x => x.Replace("#", "%23").Replace("++", "%2B%2B")).ToList();

        }
    }

    public class Language
    {
        public string languageName { get; set; }
    }
}
