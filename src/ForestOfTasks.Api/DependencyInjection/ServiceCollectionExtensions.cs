using System.Text;
using System.Text.Json;
using ForestOfTasks.SharedKernel.Consts;
using ForestOfTasks.Application.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ForestOfTasks.Api.DependencyInjection;

internal static class ServiceCollectionExtensions
{
  public static IServiceCollection BindApiConfiguration(
    this IServiceCollection services,
    ConfigurationManager configuration)
  {
    services.Configure<JwtSettings>(configuration.GetSection(ConfigSections.Auth));
    return services;
  }
  
  public static IServiceCollection AddApiAuthentication(
    this IServiceCollection services,
    ConfigurationManager configuration,
    Serilog.ILogger logger)
  {
    var jwtSettings = configuration.GetSection(ConfigSections.Auth).Get<JwtSettings>();
    
    services
    .AddAuthentication(options =>
    {
      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings!.JwtIssuer,
        ValidAudience = jwtSettings.JwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.JwtSecret)),
        ClockSkew = TimeSpan.FromMinutes(5)
      };
      
      options.Events = new JwtBearerEvents
      {
        OnAuthenticationFailed = context =>
        {          
          var problemDetails = new ProblemDetails
          {
            Title = "Authentication failed",
            Status = StatusCodes.Status401Unauthorized,
            Detail = context.Exception.Message,
            Instance = context.HttpContext.Request.Path
          };
          
          context.Response.StatusCode = problemDetails.Status.Value;
          context.Response.ContentType = "application/json";
          
          context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
          
          logger.Error(context.Exception, "Authentication failed");
          return Task.CompletedTask;
        },
        OnChallenge = async context =>
        {
          if (context.Response.HasStarted) {
            return;
          }

          var problemDetails = new ProblemDetails
          {
            Status = StatusCodes.Status401Unauthorized,
            Title = "Unauthorized",
            Detail = "You are not authorized to access this resource.",
            Instance = context.HttpContext.Request.Path
          };

          context.HandleResponse(); // Prevent default behavior
          context.Response.StatusCode = problemDetails.Status.Value;
          context.Response.ContentType = "application/json";

          await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
        }
      };
    });
    
    return services;
  }
}
