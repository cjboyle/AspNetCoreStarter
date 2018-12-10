using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreStarter.Domain
{
    public interface IEntity
    {
        // Marker Interface
    }

    public interface IEntity<T> : IEntity
        where T : class
    {
        void OnEntityTypeCreating(EntityTypeBuilder<T> builder);
    }

    public interface IEntity<T, TKey> : IEntity<T>
        where T : class
    {
        TKey Id { get; set; }
    }
}
