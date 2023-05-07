namespace GitHub.Data
{
    public class GitHubRepositoryInfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string full_name { get; set; }
        public string language { get; set; }
        public string owner_login { get; set; }
        public DateTime created_at { get; set; }

    }
}
