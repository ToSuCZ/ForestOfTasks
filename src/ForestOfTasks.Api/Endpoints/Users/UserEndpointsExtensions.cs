using FluentValidation;
using ForestOfTasks.Api.Endpoints.Users.Login;
using ForestOfTasks.Api.Endpoints.Users.Register;

namespace ForestOfTasks.Api.Endpoints.Users;

internal static class UserEndpointsExtensions
{
  public static void MapUserRoutes(this IEndpointRouteBuilder app)
  {
    var users = app.MapGroup("/api/users");

    users.MapPost("/register", RegisterRequestHandler.HandleAsync).AllowAnonymous();
    users.MapPost("/login", LoginRequestHandler.HandleAsync).AllowAnonymous();
    
    users.MapGet("", () => Results.Ok());
  }
}
