using AspNetCoreStarter.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreStarter.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
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

        internal void Seed(Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager, Microsoft.AspNetCore.Identity.RoleManager<Microsoft.AspNetCore.Identity.IdentityRole> roleManager)
        {
            if (!Users.Any(u => u.UserName == "test123"))
            {
                var user = new ApplicationUser
                {
                    UserName = "test123",
                    NormalizedUserName = "TEST123",
                    Email = "test123@example.com",
                    NormalizedEmail = "TEST123@EXAMPLE.COM",
                    EmailConfirmed = true
                };

                user.PasswordHash = userManager.PasswordHasher.HashPassword(user, "Pa$$w0rd");

                Users.Add(user);
                SaveChanges();
            }
        }
    }
}
