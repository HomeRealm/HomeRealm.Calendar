const string defaultLaunchProfile = "https";

var builder = DistributedApplication.CreateBuilder(args);

var postgresServer = builder.AddPostgres("postgres-famman")
                            .WithDataVolume("postgres-famman")
                            .WithDbGate()
                            .WithLifetime(ContainerLifetime.Persistent);
var calendarsDb = postgresServer.AddDatabase("famman-calendars", "famman_calendars");

var calendarsApi = builder.AddProject<Projects.FamMan_Api_Calendars>("famman-api-calendars", launchProfileName: defaultLaunchProfile)
    .WaitFor(calendarsDb)
    .WithReference(calendarsDb);

builder.AddProject<Projects.FamMan>("famman")
    .WithReference(calendarsApi);



// Add Docsify documentation container using AddDockerfile (corrected path)
var docsify = builder.AddDockerfile("Documentation", "../../docsify", "Dockerfile")
    .WithHttpEndpoint(port: 3000, targetPort: 3000, name: "http")
    .WithExplicitStart();

builder.Build().Run();
