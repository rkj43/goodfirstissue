using goodfirstissue.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web; // For Url Encoding

namespace goodfirstissue
{
    public class GitHubService : IGitHubService
    {
        private readonly HttpClient _httpClient;

        public GitHubService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<GithubIssue>> GetGoodFirstIssuesAsync(string language, string sortBy)
        {
            // Define the base GitHub search endpoint
            string baseUrl = "https://api.github.com/search/issues?q=label:\"good first issue\"";

            // Add language filter
            if (!string.IsNullOrEmpty(language))
            {
                if (language.Equals("C#", StringComparison.OrdinalIgnoreCase))
                {
                    language = "C%23"; // manually encode for C#
                }
                // Use HttpUtility.UrlEncode to encode the language
                baseUrl += $"+language:"+language;
            }

            // Depending on your sorting criteria, adjust the API endpoint
            switch (sortBy)
            {
                case "updated":
                    baseUrl += "&sort=updated";
                    break;
                // You can expand this switch case to handle other sorting options
                default:
                    break;
            }
            Console.WriteLine($"URL: {baseUrl}");
            // Use HttpClient to make a GET request
            var response = await _httpClient.GetAsync(baseUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonConvert.DeserializeObject<GithubSearchResponse>(content);
                return jsonResponse.Items;
            }
            else
            {
                // Handle the error or throw an exception based on the response status
                throw new Exception($"Failed to fetch data from GitHub. Status: {response.StatusCode}");
            }
        }

        // Create an auxiliary class to capture the GitHub API response structure
        public class GithubSearchResponse
        {
            public List<GithubIssue> Items { get; set; }
        }
    }
}
