using FluentResults;
using ForestOfTasks.Application.Users.Contracts;
using ForestOfTasks.Domain.Aggregates.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ForestOfTasks.Application.Users.Queries.UserDetail;

internal sealed class UserDetailHandler(
  UserManager<ApplicationUser> userManager
) : IRequestHandler<UserDetailQuery, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(UserDetailQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString());

        if (user is null)
        {
            return Result.Fail("User not found");
        }

        return Result.Ok(UserDto.FromUser(user));
    }
}
