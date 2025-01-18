using ForestOfTasks.Domain.Aggregates.UserAggregate;

namespace ForestOfTasks.Application.Users.Contracts;

public record UserDto(Guid Id, string Email, string Username)
{
  public static UserDto FromUser(ApplicationUser user) => new (
    Guid.Parse(user.Id),
    user.Email!,
    user.UserName!);
};
