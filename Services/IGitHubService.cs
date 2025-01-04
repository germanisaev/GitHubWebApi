namespace PhoenixAPI.Services;

public interface IGitHubService
{
    Task<dynamic> GetRepositoriesAsync(string query);
}
