using ForestOfTasks.Domain.Aggregates.UserAggregate;
using ForestOfTasks.Infrastructure.Consts;
using ForestOfTasks.Infrastructure.Data;
using ForestOfTasks.SharedKernel;
using ForestOfTasks.SharedKernel.Consts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ForestOfTasks.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
      this IServiceCollection services,
      ConfigurationManager configuration,
      ILogger logger,
      IHostEnvironment environment)
    {
        string? connectionString = configuration.GetConnectionString(ConnectionStrings.AspireDatabase);

        if (environment.IsEnvironment("Testing"))
        {
            services.AddDbContext<ApplicationDbContext>(
              options => options.UseInMemoryDatabase("E2ETests"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(connectionString));
        }

        services.AddIdentityCore<ApplicationUser>()
          .AddEntityFrameworkStores<ApplicationDbContext>();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 8;

            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;
        });

        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        logger.Information("[Init] {Layer} layer services registered", LayerStructure.Infrastructure);

        return services;
    }
}
