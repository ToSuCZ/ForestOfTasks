using System.Globalization;
using FluentValidation;
using ForestOfTasks;
using ForestOfTasks.Api.DependencyInjection;
using ForestOfTasks.Application.DependencyInjection;
using ForestOfTasks.Domain.DependencyInjection;
using ForestOfTasks.Infrastructure.DependencyInjection;
using ForestOfTasks.SharedKernel.Consts;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Serilog;

var logger = Log.Logger = new LoggerConfiguration()
  .Enrich.FromLogContext()
  .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture)
  .CreateLogger();

logger.Information("[Init] Starting web host");

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Configuration.AddEnvironmentVariables();
builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

builder.Services.BindApiConfiguration(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddApiAuthentication(builder.Configuration, logger);
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);

builder.Services.AddHealthChecks();

builder.Services.AddOpenApi();
builder.Services.AddSwaggerCustomization();
logger.Information("[Init] {Layer} layer services registered", LayerStructure.Api);

builder.Services
  .AddApplication(builder.Configuration, logger)
  .AddDomain(builder.Configuration, logger)
  .AddInfrastructure(builder.Configuration, logger, builder.Environment);

builder.Services.AddControllers();

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler(error =>
{
    error.Run(async ctx =>
    {
        var problemDetails = new ProblemDetails
        {
            Title = "An error occurred",
            Status = StatusCodes.Status500InternalServerError,
            Detail = ctx.Features.Get<IExceptionHandlerFeature>()?.Error.Message,
            Instance = ctx.Request.Path
        };

        ctx.Response.StatusCode = problemDetails.Status.Value;
        ctx.Response.ContentType = "application/problem+json";

        await ctx.Response.WriteAsJsonAsync(problemDetails);
    });
});

app.UseAuthentication()
  .UseAuthorization();

app.MapHealthChecks("/api/health");

app.MapControllers();

logger.Information("[Init] Application starting");

await app.RunAsync();

#pragma warning disable S1118
public partial class Program;
#pragma warning restore S1118
