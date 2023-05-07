using System.ComponentModel.DataAnnotations;

namespace GitHubRestAPI.Model
{
    public class LanguagesDTO
    {
        [Required]
        [MaxLength(5)]
        public IEnumerable<string> languageList { get; set; }
    }
}
