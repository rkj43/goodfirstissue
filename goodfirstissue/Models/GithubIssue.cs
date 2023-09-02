namespace goodfirstissue.Models
{
    public class GithubIssue
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string RepositoryUrl { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
    public class GithubResponse
    {
        public int TotalCount { get; set; }
        public bool IncompleteResults { get; set; }
        public List<GithubIssue> Items { get; set; }
    }
}
