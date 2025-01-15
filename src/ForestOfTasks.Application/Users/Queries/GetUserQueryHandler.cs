using Ardalis.Result;
using ForestOfTasks.Application.Users.Contracts;
using MediatR;

namespace ForestOfTasks.Application.Users.Queries;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, Result<UserDto>>
{
  public async Task<Result<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
  {
    await Task.CompletedTask;
    return Result.Success();
  }
}
