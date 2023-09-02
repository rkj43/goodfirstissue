using Microsoft.AspNetCore.Mvc;

namespace goodfirstissue.Controllers
{
    public class IssuesController : ControllerBase
    {
        private readonly IGitHubService _gitHubService;

        public IssuesController(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGoodFirstIssues(string language, string sortBy)
        {
            var issues = await _gitHubService.GetGoodFirstIssuesAsync(language, sortBy);
            return Ok(issues);
        }
    }

}
