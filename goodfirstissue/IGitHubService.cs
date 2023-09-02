using goodfirstissue.Models;

namespace goodfirstissue
{
    public interface IGitHubService
    {
        Task<IEnumerable<GithubIssue>> GetGoodFirstIssuesAsync(string language, string sortBy);
    }
}
