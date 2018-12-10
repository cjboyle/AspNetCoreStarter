using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreStarter.Domain
{
    public class Award : IEntity<Award, int>
    {
        [Key]
        public int Id { get; set; }

        public string Category { get; set; }

        public string Organization { get; set; }

        [ForeignKey(nameof(ActorId))]
        public Actor Actor { get; set; }
        public int? ActorId { get; set; }

        [ForeignKey(nameof(Movie)), Required]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public void OnEntityTypeCreating(EntityTypeBuilder<Award> builder)
        {
            throw new System.NotImplementedException();
        }
    }
}