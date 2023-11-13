using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using MyProject.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.AreaViewLocationFormats.Clear();
    options.AreaViewLocationFormats.Add("/Modules/{2}/Views/{1}/{0}.cshtml");
    options.AreaViewLocationFormats.Add("/Modules/{2}/Views/Shared/{0}.cshtml");
    options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
});


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddTransient<IOperationTransient, MyOperation>();
builder.Services.AddScoped<IOperationScoped, MyOperation>();
builder.Services.AddSingleton<IOperationSingleton, MyOperation>();
builder.Services.AddSingleton<IOperationSingletonInstance>(new MyOperation(Guid.Empty));

builder.Services.AddTransient<MyOperationService>();

builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

//app.MapAreaControllerRoute("AreaProducts", "Products", "Products/{controller=Products}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
