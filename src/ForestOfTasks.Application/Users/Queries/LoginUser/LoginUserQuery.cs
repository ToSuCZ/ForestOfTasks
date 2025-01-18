using FluentResults;
using ForestOfTasks.SharedKernel;

namespace ForestOfTasks.Application.Users.Queries.LoginUser;

public record LoginUserQuery(string Email, string Password) : IQuery<Result<string>>;
