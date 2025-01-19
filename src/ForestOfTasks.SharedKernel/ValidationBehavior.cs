using FluentResults;
using FluentValidation;
using MediatR;

namespace ForestOfTasks.SharedKernel;

public class ValidationBehavior<TRequest, TResponse>(
  IEnumerable<IValidator<TRequest>> validators
) : IPipelineBehavior<TRequest, TResponse>
  where TRequest : IRequest<TResponse>
  where TResponse : Result, new()
{
  public async Task<TResponse> Handle(
      TRequest request,
      RequestHandlerDelegate<TResponse> next,
      CancellationToken cancellationToken)
  {
    ArgumentNullException.ThrowIfNull(next);
    
    if (!validators.Any())
    {
      return await next();
    }
    
    var context = new ValidationContext<TRequest>(request);
    
    var validationResults = await Task.WhenAll(
      validators.Select(v => v.ValidateAsync(context, cancellationToken)));
    
    var resultErrors = validationResults
      .SelectMany(r => r.Errors)
      .Where(f => f != null)
      .ToList();

    if (resultErrors.Count != 0)
    {
        var response = new TResponse();
        response.WithErrors(resultErrors.Select(f => new Error(f.ErrorMessage)));
        return response;
    }

    
    return await next();
  }
}
