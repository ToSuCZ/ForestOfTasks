using Ardalis.Result;
using MediatR;

namespace ForestOfTasks.Application.Users.Queries;

public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, Result<string>>
{
  public async Task<Result<string>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
  {
    await Task.CompletedTask;
    return Result.Success();
  }
}
