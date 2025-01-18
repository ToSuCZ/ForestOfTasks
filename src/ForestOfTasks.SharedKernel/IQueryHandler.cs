using MediatR;

namespace ForestOfTasks.SharedKernel;

public interface IQueryHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
  where TRequest : IQuery<TResponse>
{
  
}
