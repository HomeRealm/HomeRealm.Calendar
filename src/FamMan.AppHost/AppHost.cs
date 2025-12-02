var builder = DistributedApplication.CreateBuilder(args);

var postgresServer = builder.AddPostgres("postgres-famman")
                            .WithDataVolume("postgres-famman")
                            .WithLifetime(ContainerLifetime.Persistent);
var eventsDb = postgresServer.AddDatabase("famman-events", "famman_events");


var eventsApi = builder.AddProject<Projects.FamMan_Api_Events>("famman-api-events")
    .WithReference(eventsDb)
    .WaitFor(eventsDb);


builder.AddProject<Projects.FamMan>("famman")
    .WithReference(eventsApi);


builder.Build().Run();
