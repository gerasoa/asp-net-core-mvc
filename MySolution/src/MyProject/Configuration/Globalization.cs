using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;

namespace MyProject.Configuration
{
    public static class Globalization
    {
        // o asp net assume a cultura do browser. Para forcar uma cultura independente da cultura do browser
        //public static WebApplication useGlobalizationConfig (this WebApplication app) 
        //{
        //    var defaultCulture = new CultureInfo ("pt-BR");

        //    var localizationOptions = new RequestLocalizationOptions
        //    {
        //        DefaultRequestCulture = new RequestCulture(defaultCulture),
        //        SupportedCultures = new List<CultureInfo> { defaultCulture },
        //        SupportedUICultures = new List<CultureInfo> { defaultCulture }
        //    };

        //    app.UseRequestLocalization (localizationOptions);

        //    return app;
        //}

        public static WebApplication useGlobalizationConfig(this WebApplication app)
        {
            var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localizationOptions.Value);            

            return app;
        }

        public static WebApplicationBuilder AddGlobalizationConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var suportedCultures = new[] { "en-GB", "pt-BR" };
                options.SetDefaultCulture(suportedCultures[0])
                .AddSupportedCultures(suportedCultures)
                .AddSupportedUICultures(suportedCultures);
            });

            return builder;
        }
    }
}
