using Microsoft.AspNetCore.Mvc.Razor;
using MyProject.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddGlobalizationConfig()
    .AddMvcConfiguration()
    .AddIdentityConfiguration()
    .AddDependencyInjectionConfig();

var app = builder.Build();

app.UseMvcConfiguration();

app.Run();
