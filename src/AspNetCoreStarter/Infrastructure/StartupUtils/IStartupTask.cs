using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreStarter.Infrastructure.StartupUtils
{
    public interface IStartupTask
    {
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}