using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreStarter
{
    /// <summary>
    /// Represents a Startup implementation for integration testing purposes
    /// </summary>
    public class TestStartup : Startup
    {
        /// <summary>
        /// Initializes an implementation of the Startup class that may be used for testing purposes.
        /// <para/>
        /// For example, one benefit is setting up an in-memory database provider for integration tests and automated UI tests.
        /// </summary>
        /// <param name="configuration"></param>
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
            IsTestEnvironment = true;
        }

        // Uncomment the following to set up (or remove) other services

        //public new void ConfigureServices(IServiceCollection services)
        //{
        //    base.ConfigureServices(services);
        //
        //}

        //public new void Configure(IApplicationBuilder app, IHostingEnvironment env)
        //{
        //    base.Configure(app, env);
        //
        //}
    }
}
