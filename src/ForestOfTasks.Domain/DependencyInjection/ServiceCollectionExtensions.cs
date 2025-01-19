using ForestOfTasks.SharedKernel.Consts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace ForestOfTasks.Domain.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(
      this IServiceCollection services,
      ConfigurationManager configuration,
      ILogger logger)
    {
        logger.Information("[Init] {Layer} layer services registered", LayerStructure.Domain);

        return services;
    }
}
