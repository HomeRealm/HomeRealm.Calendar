const string defaultLaunchProfile = "https";

var builder = DistributedApplication.CreateBuilder(args);

var postgresServer = builder.AddPostgres("postgres-HomeRealm")
                            .WithDataVolume("postgres-HomeRealm")
                            .WithDbGate()
                            .WithLifetime(ContainerLifetime.Persistent);
var calendarsDb = postgresServer.AddDatabase("HomeRealm-calendars", "HomeRealm_calendars");

var calendarsApi = builder.AddProject<Projects.HomeRealm_Api_Calendars>("HomeRealm-api-calendars", launchProfileName: defaultLaunchProfile)
    .WaitFor(calendarsDb)
    .WithReference(calendarsDb);

builder.AddProject<Projects.HomeRealm>("HomeRealm")
    .WithReference(calendarsApi);



// Add Docsify documentation container using AddDockerfile (corrected path)
var docsify = builder.AddDockerfile("Documentation", "../../docsify", "Dockerfile")
    .WithHttpEndpoint(port: 3000, targetPort: 3000, name: "http")
    .WithExplicitStart();

builder.Build().Run();

