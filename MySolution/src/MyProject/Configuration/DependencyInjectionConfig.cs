using Microsoft.AspNetCore.Mvc.DataAnnotations;
using MyProject.Extensions;
using MyProject.Services;

namespace MyProject.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static WebApplicationBuilder AddDependencyInjectionConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddTransient<IOperationTransient, MyOperation>();
            builder.Services.AddScoped<IOperationScoped, MyOperation>();
            builder.Services.AddSingleton<IOperationSingleton, MyOperation>();
            builder.Services.AddSingleton<IOperationSingletonInstance>(new MyOperation(Guid.Empty));
            
            builder.Services.AddTransient<MyOperationService>();

            builder.Services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();

            return builder;
        }
    }
}
