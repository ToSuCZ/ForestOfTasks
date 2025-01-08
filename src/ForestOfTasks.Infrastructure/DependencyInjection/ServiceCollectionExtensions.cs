using Microsoft.Extensions.DependencyInjection;

namespace ForestOfTasks.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection services)
  {
    return services;
  }
}
