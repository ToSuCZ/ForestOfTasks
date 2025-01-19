using ForestOfTasks.SharedKernel;
using ForestOfTasks.SharedKernel.Consts;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace ForestOfTasks.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(
      this IServiceCollection services,
      ConfigurationManager configuration,
      ILogger logger)
    {
        services.AddMediatR(options =>
          options.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly));

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        logger.Information("[Init] {Layer} layer services registered", LayerStructure.Application);

        return services;
    }
}
