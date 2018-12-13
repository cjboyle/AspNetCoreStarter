using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreStarter.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreStarter.Tests
{
    public abstract class DatabaseFixtureBase<TModel> : FixtureBase<TModel>, IDisposable
    {
        private ApplicationDbContext _dbContext;
        public virtual ApplicationDbContext DbContext
        {
            get
            {
                if (_dbContext is null)
                {
                    var options = new DbContextOptionsBuilder()
                        .UseInMemoryDatabase($"xunit_test_database_for_{typeof(TModel).Name}_Fixture")
                        .Options;

                    _dbContext = new ApplicationDbContext(options);
                    _dbContext.Database.BeginTransactionAsync();
                }
                return _dbContext;
            }
        }

        public void Dispose()
        {
            _dbContext.Database.RollbackTransaction();
            _dbContext.Dispose();
            _dbContext = null;
        }
    }
}
