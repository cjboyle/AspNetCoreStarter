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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            IsForTestingOnly = false;
        }

        public IConfiguration Configuration { get; }

        public bool IsForTestingOnly { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Setup custom MVC options
            services.AddCustomMvc();

            // Setup database access with Entity Framework
            services.AddCustomEntityFramework<ApplicationDbContext>(IsForTestingOnly);

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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

        public static Startup GetTestStartup(IConfiguration config) => new Startup(config) { IsForTestingOnly = true };
    }
}
