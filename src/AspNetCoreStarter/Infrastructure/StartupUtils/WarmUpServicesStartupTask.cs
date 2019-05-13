using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreStarter.Infrastructure.StartupUtils
{
    public class WarmUpServicesStartupTask : IStartupTask
    {
        private readonly IServiceCollection _services;
        private readonly IServiceProvider _provider;

        public WarmUpServicesStartupTask(IServiceCollection services, IServiceProvider provider)
        {
            _services = services;
            _provider = provider;
        }

        public Task ExecuteAsync(CancellationToken cancellationToken)
        {
            List<string> traces = new List<string>();
            using (var scope = _provider.CreateScope())
            {
                foreach (var serviceType in GetServices(_services))
                {
                    try
                    {
                        scope.ServiceProvider.GetServices(serviceType);
                    }
                    catch (Exception e)
                    {
                        traces.Add(e.Message);
                    }
                }
            }

            return Task.CompletedTask;
        }

        private static IEnumerable<Type> GetServices(IServiceCollection services)
        {
            return services
                .Where(desc => desc.ImplementationType != typeof(WarmUpServicesStartupTask)
                            && !desc.ServiceType.ContainsGenericParameters)
                .Select(desc => desc.ServiceType)
                .Distinct();
        }
    }
}
