var builder = DistributedApplication.CreateBuilder(args);

var postgresServer = builder.AddPostgres("postgres-famman")
                            .WithDataVolume("postgres-famman")
                            .WithDbGate()
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



// Add Docsify documentation container using AddDockerfile (corrected path)
var docsify = builder.AddDockerfile("Documentation", "../../docsify", "Dockerfile")
    .WithHttpEndpoint(port: 3000, targetPort: 3000, name: "http")
    .WithExplicitStart();

builder.Build().Run();
