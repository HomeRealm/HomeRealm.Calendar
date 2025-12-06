var builder = DistributedApplication.CreateBuilder(args);

var postgresServer = builder.AddPostgres("postgres-famman")
                            .WithDataVolume("postgres-famman")
                            .WithLifetime(ContainerLifetime.Persistent);
var eventsDb = postgresServer.AddDatabase("famman-events", "famman_events");
var choresDb = postgresServer.AddDatabase("famman-chores", "famman_chores");


var eventsApi = builder.AddProject<Projects.FamMan_Api_Events>("famman-api-events")
    .WithReference(eventsDb)
    .WaitFor(eventsDb);

var choresApi = builder.AddProject<Projects.FamMan_Api_Chores>("famman-api-chores")
    .WaitFor(choresDb)
    .WithReference(choresDb);

builder.AddProject<Projects.FamMan>("famman")
    .WithReference(eventsApi)
    .WithReference(choresApi);


builder.Build().Run();
