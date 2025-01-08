using Microsoft.Extensions.DependencyInjection;

namespace ForestOfTasks.Domain.DependencyInjection;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddDomain(this IServiceCollection services)
  {
    return services;
  }
}
