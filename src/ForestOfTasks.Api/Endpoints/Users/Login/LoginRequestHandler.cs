using FluentValidation;
using ForestOfTasks.Application.Users.Queries;
using ForestOfTasks.Domain.Aggregates.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ForestOfTasks.Api.Endpoints.Users.Login;

internal static class LoginRequestHandler
{
  public static async Task<IResult> HandleAsync(
    LoginRequest request,
    ISender mediator,
    CancellationToken ct = default)
  {
    var result = await mediator.Send(
      new LoginUserQuery(request.Email, request.Password),
      ct);

    if (!result.IsSuccess)
    {
      return Results.Unauthorized();
    }
    
    return Results.Ok($"Token: {result.Value}");
  }
}
