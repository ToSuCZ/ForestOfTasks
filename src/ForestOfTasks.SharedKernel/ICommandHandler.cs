﻿using MediatR;

namespace ForestOfTasks.SharedKernel;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
  where TCommand : ICommand<TResponse>
{

}
