var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.FamMan>("famman");

builder.AddProject<Projects.FamMan_Api_Events>("famman-api-events");

builder.Build().Run();
