using ForestOfTasks.Api.DependencyInjection;
using ForestOfTasks.Application.DependencyInjection;
using ForestOfTasks.Domain.DependencyInjection;
using ForestOfTasks.Infrastructure.DependencyInjection;
using ForestOfTasks.SharedKernel.Consts;
using Serilog;

var logger = Log.Logger = new LoggerConfiguration()
  .Enrich.FromLogContext()
  .WriteTo.Console()
  .CreateLogger();

logger.Information("[Init] Starting web host");

var builder = WebApplication.CreateBuilder(args);
{
  builder.Configuration.AddEnvironmentVariables();
  builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));
  
  builder.Services.BindApiConfiguration(builder.Configuration);
  builder.Services.AddAuthorization();
  builder.Services.AddApiAuthentication(builder.Configuration, logger);
  
  builder.Services.AddOpenApi();
  logger.Information("[Init] {layer} layer services registered", Structure.Api);
  
  builder.Services
    .AddApplication(builder.Configuration, logger)
    .AddDomain(builder.Configuration, logger)
    .AddInfrastructure(builder.Configuration, logger);
}

var app = builder.Build();
{
  app.UseHttpsRedirection();
  
  if (app.Environment.IsDevelopment())
  {
    app.MapOpenApi();
  }
  
  app.UseAuthentication()
    .UseAuthorization();
}


logger.Information("[Init] Application starting");
app.Run();
