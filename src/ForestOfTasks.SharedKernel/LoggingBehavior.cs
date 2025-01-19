using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ForestOfTasks.SharedKernel;

public class LoggingBehavior<TRequest, TResponse>(
  ILogger<Mediator> logger
) : IPipelineBehavior<TRequest, TResponse>
  where TRequest : IRequest<TResponse>
{
  private static readonly Action<ILogger, string, Exception?> _logStart =
    LoggerMessage.Define<string>(
      LogLevel.Information,
      new EventId(1, typeof(TRequest).Name),
      "Handling {Request}");
  
  private static readonly Action<ILogger, string, TResponse, double, Exception?> _logEnd =
    LoggerMessage.Define<string, TResponse, double>(
      LogLevel.Information,
      new EventId(1, typeof(TRequest).Name),
      "Handled {Request} with {Response} in {Ms} ms");
  
  public async Task<TResponse> Handle(
    TRequest request,
    RequestHandlerDelegate<TResponse> next,
    CancellationToken cancellationToken)
  { 
    ArgumentNullException.ThrowIfNull(next);
    
    _logStart(logger, typeof(TRequest).Name, null);
    var sw = Stopwatch.StartNew();
    
    var response = await next();
    
    _logEnd(logger, typeof(TRequest).Name, response, sw.ElapsedMilliseconds, null);
    sw.Stop();
    return response;
  }
}
