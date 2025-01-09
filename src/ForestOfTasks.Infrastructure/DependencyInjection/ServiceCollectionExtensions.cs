using ForestOfTasks.Domain.Aggregates.User;
using ForestOfTasks.Infrastructure.Data;
using ForestOfTasks.SharedKernel;
using ForestOfTasks.SharedKernel.Consts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace ForestOfTasks.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    ConfigurationManager configuration,
    ILogger logger)
  {
    string? connectionString = configuration.GetConnectionString("Users");
    services.AddDbContext<ForestOfTasksDbContext>(
      options => options.UseSqlServer(connectionString));

    services.AddIdentityCore<User>()
      .AddEntityFrameworkStores<ForestOfTasksDbContext>();
    
    services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

    logger.Information("[Init] {layer} layer services registered", Structure.Infrastructure);
    
    return services;
  }
}
