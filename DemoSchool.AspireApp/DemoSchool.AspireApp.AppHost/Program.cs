var builder = DistributedApplication.CreateBuilder(args);

var webapi = builder.AddProject<Projects.DemoSchool_WebAPI>("demoschool.webapi");

builder.AddProject<Projects.DemoSchool_WebUI>("demoschool.webui")
       .WithReference(webapi);

builder.Build().Run();
