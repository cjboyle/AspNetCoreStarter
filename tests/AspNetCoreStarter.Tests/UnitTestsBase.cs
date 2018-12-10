using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AspNetCoreStarter.Tests
{
    public abstract class UnitTestsBase<TFixture> : IClassFixture<FixtureBase>
    {
    }
}
