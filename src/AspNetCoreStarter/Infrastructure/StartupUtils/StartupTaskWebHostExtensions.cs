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
        public static async Task RunWithTasksAsync(this IWebHost webHost)
        {
            var startupTasks = webHost.Services.GetServices<IStartupTask>();
            var tasks = new List<Task>();

            foreach (var st in startupTasks)
            {
                tasks.Add(Task.Run(() => st.ExecuteAsync(default)));
            }

            Task.WaitAll(tasks.ToArray());
            await webHost.RunAsync();
        }
    }
}
