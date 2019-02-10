using AspNetCoreStarter.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreStarter.Infrastructure.StartupUtils
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Extension helper to configure the MVC framework (e.g. configuring Vertical-Slice Architecture for Razor views)
        /// </summary>
        /// <param name="services"></param>
        /// <param name="target">The ASP.NET Core compatibility version for the application.</param>
        /// <returns></returns>
        public static IServiceCollection AddFeatureSlicedMvc(this IServiceCollection services, CompatibilityVersion target = CompatibilityVersion.Latest)
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
                .AddViewOptions(viewOptions =>
                {
                    viewOptions.HtmlHelperOptions.ClientValidationEnabled = true;
                    viewOptions.AllowRenderingMaxLengthAttribute = true;
                })
                .SetCompatibilityVersion(target);

            return services;
        }

        /// <summary>
        /// Extension helper to configure Entity Framework and database services for the application.
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="services"></param>
        /// <param name="isTestEnvironment">True if running in a test environment (e.g. Xunit); otherwise false.</param>
        /// <returns></returns>
        public static IServiceCollection AddCustomEntityFramework<TDbContext>(this IServiceCollection services, bool isTestEnvironment = false) 
            where TDbContext : DbContext
        {
            var connectionString = Startup.DefaultConnectionString;

            if (isTestEnvironment)
            {
                services.AddEntityFrameworkInMemoryDatabase();
                services.AddDbContext<TDbContext>();
            }
            else
            {
                // Add a persistent database such as SQL Server, LocalDB, PostgreSQL, etc.
                //services.AddEntityFrameworkNpgsql(); // Using PostgreSQL driver
                //services.AddDbContext<TDbContext>(builder => builder.UseNpgsql(connectionString));
                services.AddEntityFrameworkSqlServer();
                services.AddDbContext<TDbContext>(builder => builder.UseSqlServer(connectionString));
            }

            return services;
        }

        /// <summary>
        /// Extension helper to add Entity Framework and Identity services to the application (sets account requirements like password length, etc.)
        /// </summary>
        /// <typeparam name="TIdentityDbContext"></typeparam>
        /// <typeparam name="TIdentityUser"></typeparam>
        /// <param name="services"></param>
        /// <param name="isTestEnvironment">True if running in a test environment (e.g. Xunit); otherwise false.</param>
        /// <returns></returns>
        public static IServiceCollection AddCustomEntityFrameworkWithIdentity<TIdentityDbContext, TIdentityUser>(this IServiceCollection services, bool isTestEnvironment = false)
            where TIdentityDbContext : IdentityDbContext<TIdentityUser>
            where TIdentityUser : IdentityUser
        {
            services.AddCustomEntityFramework<TIdentityDbContext>(isTestEnvironment);
            services.AddDefaultIdentity<TIdentityUser>(options =>
            {
                // Set password requirements
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;

                // Set account lockout rules
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                options.Lockout.MaxFailedAccessAttempts = 10;

                // Set account creation requirements
                options.User.RequireUniqueEmail = true;

                // 2FA providers, email confirmation, etc.

            }).AddEntityFrameworkStores<ApplicationDbContext>();

            // Services for securing the application (uncomment as needed)
            // See example with separate partial class at https://github.com/samueleresca/Blog.TokenAuthGettingStarted
            //services.AddAuthentication()
            //    .AddJwtBearer()
            //    .AddGoogle()
            //    .AddMicrosoftAccount();
            //services.AddAuthorization();

            return services;
        }
    }
}
