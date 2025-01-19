using FluentResults;
using ForestOfTasks.Application.Users.Contracts;
using ForestOfTasks.SharedKernel;

namespace ForestOfTasks.Application.Users.Queries.UserDetail;

public record UserDetailQuery(Guid UserId) : IQuery<Result<UserDto>>;
