using ForestOfTasks.SharedKernel.Consts;
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
    
    logger.Information("[Init] {layer} layer services registered", Structure.Application);
    
    return services;
  }
}
