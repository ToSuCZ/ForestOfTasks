using ForestOfTasks.AppHost.constants;

var builder = DistributedApplication.CreateBuilder(args);

var mssql = builder.AddSqlServer(AppNames.Db)
    .WithDataVolume();

var api = builder.AddProject<Projects.ForestOfTasks_Api>(AppNames.Api)
    .WithReference(mssql)
    .WaitFor(mssql)
    .WithExternalHttpEndpoints();

builder.AddNpmApp(AppNames.Frontend,"../ForestOfTasks.Presentation")
    .WithReference(api)
    .WaitFor(api)
    .WithExternalHttpEndpoints()
    .WithEnvironment("BROWSER", "none")
    .PublishAsDockerFile();

await builder.Build().RunAsync();
