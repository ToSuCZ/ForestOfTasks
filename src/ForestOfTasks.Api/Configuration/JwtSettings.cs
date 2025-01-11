namespace ForestOfTasks.Api.Configuration;

internal class JwtSettings
{
  public string JwtIssuer { get; set; } = string.Empty;
  public string JwtAudience { get; set; } = string.Empty;
  public string JwtSecret { get; set; } = string.Empty;
}
