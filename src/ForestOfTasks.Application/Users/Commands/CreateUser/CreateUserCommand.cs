using FluentResults;
using ForestOfTasks.Domain.Aggregates.UserAggregate;
using ForestOfTasks.SharedKernel;

namespace ForestOfTasks.Application.Users.Commands.CreateUser;

public record CreateUserCommand(
  string Email,
  string Username,
  string Password) : ICommand<Result<ApplicationUser>>;