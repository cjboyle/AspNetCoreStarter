using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreStarter.Infrastructure.StartupUtils
{
    public static class StartupTaskWebHostExtensions
    {
        public static async Task RunWithTasksAsync(this IWebHost webHost, CancellationToken cancellationToken = default)
        {
            var tasks = webHost.Services.GetServices<IStartupTask>();

            foreach (var task in tasks)
            {
                await task.ExecuteAsync(cancellationToken);
            }

            await webHost.RunAsync(cancellationToken);
        }
    }
}
