using AspNetCoreStarter.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreStarter.Domain
{
    public class Actor : IEntity<Actor, int>
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<CastingCredit> Roles { get; set; }
        public virtual ICollection<Award> Awards { get; set; }

        public void OnEntityTypeCreating(EntityTypeBuilder<Actor> builder)
        {
            throw new NotImplementedException();
        }
    }
}
