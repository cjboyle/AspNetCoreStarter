using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AspNetCoreStarter.Tests
{
    public abstract class UnitTestsBase<TFixture> : IClassFixture<TFixture>
        where TFixture : class, IFixture
    {
        public TFixture Fixture { get; }

        public UnitTestsBase(TFixture fixture)
        {
            Fixture = fixture;
        }
    }
}
