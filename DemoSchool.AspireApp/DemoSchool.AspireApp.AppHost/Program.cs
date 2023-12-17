var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.DemoSchool_WebAPI>("demoschool.webapi");

builder.AddProject<Projects.DemoSchool_WebUI>("demoschool.webui");

builder.Build().Run();
