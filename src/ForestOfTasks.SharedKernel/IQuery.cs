﻿using MediatR;

namespace ForestOfTasks.SharedKernel;

public interface IQuery<out TResponse> : IRequest<TResponse>
{

}
