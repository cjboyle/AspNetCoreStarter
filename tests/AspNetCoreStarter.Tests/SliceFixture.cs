using AspNetCoreStarter.Domain;
using AspNetCoreStarter.Infrastructure;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreStarter.Tests
{
    public class SliceFixture
    {
        private static readonly IHostingEnvironment _host;
        private static readonly IConfiguration _configuration;
        private static readonly IServiceProvider _rootContainer;
        private static readonly IServiceScopeFactory _scopeFactory;
        // Use Respawn checkpoints to store database clean states if testing with a persistent/production-like database
        //private static readonly Checkpoint _checkpoint;

        static SliceFixture()
        {
            _host = A.Fake<IHostingEnvironment>();
            A.CallTo(() => _host.ContentRootPath).Returns(Directory.GetCurrentDirectory());

            var startup = new TestStartup(new ConfigurationBuilder().Build());
            _configuration = Startup.Configuration;

            var services = new ServiceCollection();
            startup.ConfigureServices(services);

            _rootContainer = services.BuildServiceProvider();
            _scopeFactory = _rootContainer.GetService<IServiceScopeFactory>();
            //_checkpoint = new Checkpoint();
        }

        public static async Task ExecuteScopeAsync(Func<IServiceProvider, Task> action)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

                try
                {
                    dbContext.Database.BeginTransaction();

                    await action(scope.ServiceProvider);

                    dbContext.Database.CommitTransaction();
                }
                catch (Exception)
                {
                    dbContext.Database.RollbackTransaction();
                    throw;
                }
            }
        }

        public static async Task<T> ExecuteScopeAsync<T>(Func<IServiceProvider, Task<T>> action)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

                try
                {
                    dbContext.Database.BeginTransaction();

                    var result = await action(scope.ServiceProvider);

                    dbContext.Database.CommitTransaction();

                    return result;
                }
                catch (Exception)
                {
                    dbContext.Database.RollbackTransaction();
                    throw;
                }
            }
        }

        public static Task ExecuteDbContextAsync(Func<ApplicationDbContext, Task> action)
        {
            return ExecuteScopeAsync(sp => action(sp.GetService<ApplicationDbContext>()));
        }

        public static Task<T> ExecuteDbContextAsync<T>(Func<ApplicationDbContext, Task<T>> action)
        {
            return ExecuteScopeAsync(sp => action(sp.GetService<ApplicationDbContext>()));
        }

        public static Task InsertAsync(params IEntity[] entities)
        {
            return ExecuteDbContextAsync(db =>
            {
                foreach (var entity in entities)
                {
                    //db.Set(entity.GetType()).Add(entity);
                }
                return db.SaveChangesAsync();
            });
        }

        public static Task<T> FindAsync<T, TKey>(Func<T, TKey> keySelector)
            where T : class, new()
        {
            return ExecuteDbContextAsync(db => db.Set<T>().FindAsync(keySelector.Invoke(new T())));
        }

        public static Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            return ExecuteScopeAsync(sp =>
            {
                var mediator = sp.GetService<IMediator>();

                return mediator.Send(request);
            });
        }

        public static Task SendAsync(IRequest request)
        {
            return ExecuteScopeAsync(sp =>
            {
                var mediator = sp.GetService<IMediator>();

                return mediator.Send(request);
            });
        }
    }
}
