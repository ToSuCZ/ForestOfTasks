using Microsoft.OpenApi.Models;

namespace ForestOfTasks.Api.DependencyInjection;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerCustomization(this IServiceCollection services)
    {
        const string version = "v1";

        services.AddSwaggerGen(config =>
        {
            config.SwaggerDoc(
                version,
                new OpenApiInfo
                {
                    Title = "ForestOfTasks.ServiceDefaults.Api",
                    Version = version,
                    Description = "Api of the ForestOfTasks.ServiceDefaults project",
                    Contact = new OpenApiContact { Name = "Tomas Vacula", Email = "tomas.vacula@protonmail.com" }
                });

            config.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' followed by your token in the Authorization header"
                }
            );
            
            config.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                }
            );
        });

        return services;
    }
}
