namespace GitHubRestAPI.Model
{
    public record LanguagesDTO
    {
        public IEnumerable<Language> languageList;
    }

    public record Language
    {
        public string languageName;
    }
}
