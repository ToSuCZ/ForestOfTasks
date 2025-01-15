using FluentValidation;
using ForestOfTasks.Application.Users.Commands;
using ForestOfTasks.Domain.Aggregates.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ForestOfTasks.Api.Endpoints.Users.Register;

internal static class RegisterRequestHandler
{
  public static async Task<IResult> HandleAsync(
    RegisterRequest request,
    ISender mediator,
    CancellationToken ct = default)
  {
    var result = await mediator.Send(
      new CreateUserCommand(request.Email, request.Username, request.Password),
      ct);
    
    if (!result.IsSuccess)
    {
      return Results.BadRequest(result.Errors);
    }
    
    return Results.Ok($"Registered as: {result.Value.UserName} with1 {result.Value.Email}");
  }
}
