using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MediatR;
using AspNetCoreStarter.Infrastructure;
using Microsoft.AspNetCore.SpaServices.Webpack;
using System.IO;
using AutoMapper;

namespace AspNetCoreStarter
{
    public partial class Startup
    {
        /// <summary>
        /// Initializes the web host startup environment.
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            IsTestEnvironment = false;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Setup custom MVC options
            services.AddCustomMvc();

            // Setup database access with Entity Framework
            services.AddCustomEntityFramework<ApplicationDbContext>(IsTestEnvironment);

            // Enable MediatR request handler pattern
            services.AddMediatR(typeof(Startup));

            // Enable AutoMapper service
            services.AddAutoMapper(typeof(Startup));
            Mapper.Initialize(config => config.AddProfiles(typeof(Startup)));
            Mapper.AssertConfigurationIsValid();

            // Services for securing the application (uncomment as needed)
            // See example with separate partial class at https://github.com/samueleresca/Blog.TokenAuthGettingStarted
            //services.AddAuthentication()
            //    .AddJwtBearer()
            //    .AddGoogle()
            //    .AddMicrosoftAccount();
            //services.AddIdentity<>();
            //services.AddAuthorization();
            services.AddAntiforgery();

            // Setup a logging service as needed
            //services.AddLogging();

            // Setup websockets/SignalR
            //services.AddSignalR()
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions()
                {
                    ProjectPath = Path.Combine(Directory.GetCurrentDirectory(), "ClientApp"),
                    ConfigFile = "webpack.config.dev.js",
                    HotModuleReplacement = true,
                    //ReactHotModuleReplacement = true,
                });
            }
            else
            {
                app.UseHsts();
                app.UseExceptionHandler("/Default/Error");
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Default",
                    template: "/{controller=Default}/{action=Index}/{id?}"
                );
            });

            //app.UseAuthentication(); // Uncomment if setting up authentication
        }

        /// <summary>
        /// Gets the external app configuration.
        /// </summary>
        public static IConfiguration Configuration { get; protected set; }

        /// <summary>
        /// Gets the default database connection string.
        /// Shorthand for 'Startup.Configuration.GetConnectionString("DefaultConnection")'.
        /// </summary>
        public static string DefaultConnectionString => Configuration.GetConnectionString("DefaultConnection");

        /// <summary>
        /// Returns true if the web host is being run in a test environment.
        /// (i.e. TestStartup.cs in the tests project)
        /// </summary>
        public static bool IsTestEnvironment { get; protected set; }
    }
}
