using ForestOfTasks.Application.Configuration;
using ForestOfTasks.SharedKernel.Consts;

namespace ForestOfTasks.Api.DependencyInjection;

internal static class ConfigExtensions
{
    public static IServiceCollection BindApiConfiguration(
      this IServiceCollection services,
      ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(ConfigSections.Auth));
        return services;
    }
}
