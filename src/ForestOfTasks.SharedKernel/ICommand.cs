using MediatR;

namespace ForestOfTasks.SharedKernel;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
  
}
