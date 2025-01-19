namespace ForestOfTasks.Application.Configuration;

public class JwtSettings
{
    public string JwtIssuer { get; set; } = string.Empty;
    public string JwtAudience { get; set; } = string.Empty;
    public string JwtSecret { get; set; } = string.Empty;
    public int JwtDurationInMinutes { get; set; }
}
