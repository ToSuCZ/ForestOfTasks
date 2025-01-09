namespace ForestOfTasks.Api.Configuration;

public class JwtSettings
{
  public string JwtIssuer { get; set; } = string.Empty;
  public string JwtAudience { get; set; } = string.Empty;
  public string JwtSecret { get; set; } = string.Empty;
}
