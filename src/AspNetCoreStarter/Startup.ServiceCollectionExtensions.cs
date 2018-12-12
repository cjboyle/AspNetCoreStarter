using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreStarter
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            services.AddMvc(mvcOptions =>
                {
                    mvcOptions.Conventions.Add(new FeatureControllerModelConvention());
                })
                .AddRazorOptions(razorOptions =>
                {
                    // {0} - Action Name
                    // {1} - Controller Name
                    // {2} - Area Name
                    // {3} - Feature Name
                    // Replace normal view location entirely
                    razorOptions.ViewLocationFormats.Clear();
                    razorOptions.ViewLocationFormats.Add("/Features/{3}/{0}.cshtml"); // e.g. Features/Default/Index.cshtml
                    razorOptions.ViewLocationFormats.Add("/Features/{3}/{1}/{0}.cshtml"); // e.g.
                    razorOptions.ViewLocationFormats.Add("/Features/Shared/{0}.cshtml"); // e.g. Features/Shared/Error.cshtml
                    razorOptions.ViewLocationExpanders.Add(new FeatureViewLocationExpander());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            return services;
        }

        public static IServiceCollection AddCustomEntityFramework<TDbContext>(this IServiceCollection services, bool isTestEnvironment = false) 
            where TDbContext : DbContext
        {

            if (isTestEnvironment)
            {
                services.AddDbContext<TDbContext>();
                services.AddEntityFrameworkInMemoryDatabase();
            }
            else
            {
                // Add a persistent database such as SQL Server, LocalDB, PostgreSQL, etc.
                var connectionString = Startup.DefaultConnectionString;
            }

            return services;
        }
    }
}
