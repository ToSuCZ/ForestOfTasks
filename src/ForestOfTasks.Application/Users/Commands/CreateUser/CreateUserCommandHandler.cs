using FluentResults;
using ForestOfTasks.Application.Users.Contracts;
using ForestOfTasks.Domain.Aggregates.UserAggregate;
using ForestOfTasks.SharedKernel;
using Microsoft.AspNetCore.Identity;

namespace ForestOfTasks.Application.Users.Commands.CreateUser;

internal sealed class CreateUserCommandHandler(
  UserManager<ApplicationUser> userManager
) : ICommandHandler<CreateUserCommand, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(
      CreateUserCommand request,
      CancellationToken cancellationToken)
    {

        var newUser = new ApplicationUser { UserName = request.Username, Email = request.Email };
        var result = await userManager.CreateAsync(newUser, request.Password);

        if (result.Succeeded)
        {
            return Result.Ok(UserDto.FromUser(newUser));
        }

        return Result
            .Fail(new Error("Validation Failed"))
            .WithReasons(result.Errors.Select(e => new Error(e.Description)));

    }
}
