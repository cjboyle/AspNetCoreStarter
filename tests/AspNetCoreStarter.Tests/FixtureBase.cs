﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreStarter.Tests
{
    public abstract class FixtureBase<TModel> : IFixture
    {
        public abstract TModel Model { get; set; }
    }
}
