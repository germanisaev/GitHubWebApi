
using Newtonsoft.Json; 

namespace PhoenixAPI.Services;

public class GitHubService: IGitHubService {
    
    private readonly HttpClient _httpClient;
    private readonly ILogger<GitHubService> _logger;

    public GitHubService(HttpClient httpClient, ILogger<GitHubService> logger) {
        
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task<dynamic> GetRepositoriesAsync(string query) {
        try {
            var url = $"https://api.github.com/search/repositories?q={query}";

            // Add GitHub API headers
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "PhoenixAPI");
            
            var response = await _httpClient.GetAsync(url);

            if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized) {
                throw new Exception("GitHub API returned 401 Unauthorized. Check your token.");
            }

            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();

            // Deserialize JSON to object
            var json = JsonConvert.SerializeObject(jsonResponse);
            dynamic json2 = JsonConvert.DeserializeObject(json);
            
            return json2;
        }
        catch(Exception ex) {
            _logger.LogError(ex, ex.Message);
        }
        
        return null;
    }
}
