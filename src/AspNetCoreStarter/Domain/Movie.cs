using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreStarter.Domain
{
    public class Movie : IEntity<Movie, int>
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Runtime { get; set; }
        public double ImdbRating { get; set; }
        public int RottenTomatoRating { get; set; }

        public virtual ICollection<CastingCredit> Cast { get; set; }
        public virtual ICollection<Award> Awards { get; set; }

        public void OnEntityTypeCreating(EntityTypeBuilder<Movie> builder)
        {
            
        }
    }
}
