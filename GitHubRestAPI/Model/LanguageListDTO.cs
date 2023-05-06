using System.ComponentModel.DataAnnotations;

namespace GitHubRestAPI.Model
{
    public class LanguagesDTO
    {
        [Required]
        public IEnumerable<string> languageList { get; set; }
    }
}
