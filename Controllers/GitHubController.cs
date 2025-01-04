
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoenixAPI.Services;

namespace PhoenixAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class GitHubController: ControllerBase
{
    private readonly IGitHubService _gitHubService;

    public GitHubController(IGitHubService gitHubService) { 
        _gitHubService = gitHubService;
    }

    [HttpGet("repositories")]
    public async Task<IActionResult> GetRepositories(
        [FromQuery] string query = "mywebapi") { 
        
        var repositories = await _gitHubService.GetRepositoriesAsync(query); 

        return Ok(repositories);
    }
    
}
