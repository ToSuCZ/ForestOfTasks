using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ForestOfTasks.SharedKernel;

public class LoggingBehavior<TRequest, TResponse>(
  ILogger<Mediator> logger
) : IPipelineBehavior<TRequest, TResponse>
  where TRequest : IRequest<TResponse>
{
  public async Task<TResponse> Handle(
    TRequest request,
    RequestHandlerDelegate<TResponse> next,
    CancellationToken cancellationToken)
  {
    logger.LogInformation("Handling {Request}", typeof(TRequest).Name);
    var sw = Stopwatch.StartNew();
    
    var response = await next();
    
    logger.LogInformation("Handled {Request} with {Response} in {ms} ms", typeof(TRequest).Name, response, sw.ElapsedMilliseconds);
    sw.Stop();
    return response;
  }
}
