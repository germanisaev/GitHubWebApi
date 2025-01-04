namespace PhoenixAPI.Models;

public class LoginToken
{
    public string? access_token { get; set; }
    public string? user_id { get; set; }
    public string? token_type { get; set; }
    /// <summary>
    /// expires time in seconds
    /// </summary>
    /// <value></value>
    public int? expires_in { get; set; }
}
