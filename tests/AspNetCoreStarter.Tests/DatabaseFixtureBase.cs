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
                        .UseInMemoryDatabase($"xunit_db_fixture_for_{typeof(TModel).Name}")
                        .Options;

                    _dbContext = new ApplicationDbContext(options);
                    //_dbContext.Database.BeginTransactionAsync();
                }
                return _dbContext;
            }
        }

        public void Dispose()
        {
            //DbContext.Database.RollbackTransaction();
            DbContext.Dispose();
            _dbContext = null;
        }
    }
}
