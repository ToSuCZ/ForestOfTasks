using Ardalis.Result;
using ForestOfTasks.Domain.Aggregates.UserAggregate;
using ForestOfTasks.SharedKernel;
using Microsoft.AspNetCore.Identity;

namespace ForestOfTasks.Application.Users.Commands;

public class CreateUserCommandHandler(
  UserManager<ApplicationUser> userManager
) : ICommandHandler<CreateUserCommand, Result<ApplicationUser>>
{
  public async Task<Result<ApplicationUser>> Handle(
    CreateUserCommand request,
    CancellationToken cancellationToken)
  {
    
    var newUser = new ApplicationUser { UserName = request.Username, Email = request.Email };
    var result = await userManager.CreateAsync(newUser, request.Password);
    
    if (result.Succeeded)
    {
      return Result<ApplicationUser>.Success(newUser);
    }
    
    return Result.Error();
  }
}
