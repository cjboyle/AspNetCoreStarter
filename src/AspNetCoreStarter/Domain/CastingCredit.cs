using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreStarter.Domain
{
    public class CastingCredit : IEntity<CastingCredit, int>
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ActorId")]
        public Actor Actor { get; set; }
        public int ActorId { get; set; }

        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }
        public int MovieId { get; set; }

        public string CharacterName { get; set; }

        public void OnEntityTypeCreating(EntityTypeBuilder<CastingCredit> builder)
        {
            throw new NotImplementedException();
        }
    }
}
