using AspNetCoreStarter.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreStarter.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<CastingCredit> CastingCredits { get; set; }


        /// <summary>
        /// Instantiates a database context with default options
        /// </summary>
        public ApplicationDbContext() : base()
        {
        }

        /// <summary>
        /// Instantiates a database context with the given options
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Customize the database model mappings
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var dbSets = GetType().GetProperties().Where(prop => prop.GetType().Name.Contains("DbSet"));
            foreach (var dbSet in dbSets)
            {
                //var t_type = dbSet.GetType().GenericTypeArguments[0];
                //bool isIModelCreator = t_type.GetInterfaces().Any(i => i.Name == nameof(IModelCreator));
                //if (isIModelCreator)
                //{
                //    var model = Activator.CreateInstance(t_type);
                //    t_type.GetMethod(nameof(IModelCreator.OnModelCreating)).Invoke(model, new[] { modelBuilder });
                //}
            }
        }

        /// <summary>
        /// Customize the database context
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            // Customize the ORM options
        }

        internal void Seed()
        {
            throw new NotImplementedException();
        }
    }
}
