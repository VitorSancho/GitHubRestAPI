namespace GitHub.Data
{
    public class GitHubRepositoryDetails
    {
            public int id { get; set; }
            public string name { get; set; }
            public string full_name { get; set; }
            public string language { get; set; }
            public string owner_login { get; set; }
            public string owner_id { get; set; }
            public string owner_url { get; set; }
            public string html_url { get; set; }
            public string description { get; set; }
            public string repository_url { get; set; }
            public string collaborators_url { get; set; }
            public DateTime created_at { get; set; }
            public DateTime updated_at { get; set; }
            public DateTime pushed_at { get; set; }
            public string clone_url { get; set; }
            public int stargazers_count { get; set; }
            public int watchers_count { get; set; }
            public bool has_wiki { get; set; }
            public string license_key { get; set; }
            public string license_name { get; set; }
            public string visibility { get; set; }
    }

}
