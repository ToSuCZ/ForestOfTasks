using Ardalis.Result;
using ForestOfTasks.SharedKernel;

namespace ForestOfTasks.Application.Users.Queries;

public record LoginUserQuery(string Email, string Password) : IQuery<Result<string>>;
