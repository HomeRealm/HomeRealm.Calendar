var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.FamMan>("famman");

builder.Build().Run();
