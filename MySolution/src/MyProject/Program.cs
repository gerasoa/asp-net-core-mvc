using MyProject.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddMvcConfiguration()
       .AddIdentityConfiguration()
       .AddDependencyInjectionConfig();

var app = builder.Build();

app.UseMvcConfiguration();

app.Run();
