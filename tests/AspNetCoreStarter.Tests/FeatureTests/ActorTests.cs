using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AspNetCoreStarter.Tests.FeatureTests
{
    public class ActorTests : UnitTestsBase<ActorFixture>
    {
        public ActorTests() : base(new ActorFixture())
        {
        }

        [Fact]
        public async Task ActorShouldBeSavedToDb()
        {
            Assert.Empty(Fixture.DbContext.Actors);

            await Fixture.DbContext.Actors.AddAsync(Fixture.Model);
            await Fixture.DbContext.SaveChangesAsync();

            Assert.Single(Fixture.DbContext.Actors);
        }
    }
}
