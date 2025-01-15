using Ardalis.Result;
using ForestOfTasks.Application.Users.Contracts;
using ForestOfTasks.SharedKernel;

namespace ForestOfTasks.Application.Users.Queries;

public record GetUserQuery(Guid UserId) : IQuery<Result<UserDto>>;
