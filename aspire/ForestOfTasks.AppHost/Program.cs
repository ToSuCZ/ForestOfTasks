using ForestOfTasks.AppHost.constants;

var builder = DistributedApplication.CreateBuilder(args);

var mssql = builder.AddSqlServer(AppNames.Db)
    .WithDataVolume();

var api = builder.AddProject<Projects.ForestOfTasks_Api>(AppNames.Api)
    .WithReference(mssql)
    .WaitFor(mssql)
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints();

builder.AddDockerfile(AppNames.Frontend, "../../web", "Dockerfile")
    .WithReference(api)
    .WaitFor(api)
    .WithHttpEndpoint(env: "PORT", targetPort: 3000)
    .WithExternalHttpEndpoints();

await builder.Build().RunAsync();
