using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PhoenixAPI.Models;

namespace PhoenixAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController: ControllerBase
{
    private readonly IConfiguration _config;
    
    public AuthController(IConfiguration config) {
        _config = config;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest login) {
        try {
            if(login.Username == "admin" && login.Password == "Aa123456") {
                //serializes the token into a JWT format
                var token = GenerateJwtToken(login.Username);
                
                //get the expiration times and pass them to the client
                var expires_sec = Convert.ToInt32(_config["JwtConfig:ExpiryMinutes"]) * 60;
                return Ok(new LoginToken()
                {
                    access_token = token,
                    user_id = login.Username,
                    expires_in = expires_sec,
                    token_type = "Bearer"
                });
            }
            return Unauthorized(new LoginToken()
                {
                    access_token = null,
                    user_id = null,
                    expires_in = 0,
                    token_type = "Bearer"
                });
        }
        catch (Exception ex)
        {
            throw new Exception("Login Error", ex);
        }
        
    }

    private string GenerateJwtToken(string username) {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtConfig:Secret"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            claims: claims,
            issuer: _config["JwtConfig:ValidIssuer"],
            audience: _config["JwtConfig:ValidAudience"],
            expires: DateTime.Now.AddMinutes(double.Parse(_config["JwtConfig:ExpiryMinutes"])),
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
