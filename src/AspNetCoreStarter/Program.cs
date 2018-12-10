using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreStarter.Infrastructure;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspNetCoreStarter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            ProcessArgs(args, host);
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();



        #region Argument Processing

        public static void ProcessArgs(string[] args, IWebHost host)
        {
            if (args.Contains("help") || args.Contains("?"))
            {
                Console.WriteLine($"'{new object().GetType().Assembly.FullName}' Argument Usage:\n");
                Console.WriteLine("\t help|?        Outputs this help message.");
                Console.WriteLine("\t exit          Terminates the application. If other arguments are specified, they will be processed first.");

                // ProcessDbArgs
                Console.WriteLine("\t resetdb       Resets the database to a clean state.");
                Console.WriteLine("\t dropdb        Deletes the database.");
                Console.WriteLine("\t migratedb     Applies any pending migrations to the database. Will create the database if it does not exist.");
                Console.WriteLine("\t seeddb        Seeds the database with tests data.");

                // Add argument definitions for other argument processors

                Console.WriteLine($"\n{(args.Any(s => s.Contains("db")) ? "Database operation cancelled by 'help'" : "")}\n");
                Environment.Exit(0);
            }

            // Run any command-line argument handlers here
            ProcessDbArgs(args, host);


            if (args.Contains("exit"))
            {
                Environment.Exit(0);
            }
        }


        public static void ProcessDbArgs(string[] args, IWebHost host)
        {
            var services = host.Services.GetService<IServiceScopeFactory>();

            using (var scope = services.CreateScope())
            {
                if (args.Contains("dropdb") || args.Contains("resetdb"))
                {
                    Console.WriteLine("Dropping the database");
                    new ApplicationDbContext().Database.EnsureDeleted();
                }
                if (args.Contains("migratedb") || args.Contains("resetdb"))
                {
                    Console.WriteLine("Migrating the database");
                    new ApplicationDbContext().Database.Migrate();
                }
                if (args.Contains("seeddb"))
                {
                    Console.WriteLine("Seeding the database");
                    new ApplicationDbContext().Seed();
                }
            }
        }

        #endregion
    }
}
