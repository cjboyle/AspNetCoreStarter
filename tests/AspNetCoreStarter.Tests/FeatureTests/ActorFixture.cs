using System;
using System.Collections.Generic;
using System.Text;
using AspNetCoreStarter.Domain;

namespace AspNetCoreStarter.Tests.FeatureTests
{
    public class ActorFixture : DatabaseFixtureBase<Actor>
    {
        public override Actor Model { get; set; } = new Actor { FirstName = "Ryan", MiddleName = "J", LastName = "Reynolds" };
    }
}
