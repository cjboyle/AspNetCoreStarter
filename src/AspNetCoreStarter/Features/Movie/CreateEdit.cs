using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreStarter.Infrastructure;
using AspNetCoreStarter.Infrastructure.ValidationAttributes;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreStarter.Features.Movie
{
    public class CreateEdit
    {
        /// <summary>
        /// Represents a Create/Edit query view model for movie objects
        /// </summary>
        public class Query : IRequest<Command>
        {
            [Required]
            public int? Id { get; set; }
        }

        /// <summary>
        /// Represents a query handler for creating and editing movie objects
        /// </summary>
        public class QueryHandler : IRequestHandler<Query, Command>
        {
            private readonly ApplicationDbContext _db;

            public QueryHandler(ApplicationDbContext db)
            {
                _db = db;
            }

            public async Task<Command> Handle(Query request, CancellationToken cancellationToken)
            {
                if (request is null || !_db.Movies.Any(m => m.Id == request.Id))
                {
                    return await Task.Run(() => new Command());
                }

                return await _db.Movies.Include(m => m.Cast)
                    .Where(m => m.Id == request.Id)
                    .ProjectToSingleOrDefaultAsync<Command>();
            }
        }

        /// <summary>
        /// Represents a Create/Edit command view model for movie objects
        /// </summary>
        public class Command : IRequest<int>
        {
            public int? Id { get; set; }

            [Required(ErrorMessage = "{0} is required")]
            [StringLength(50, MinimumLength = 1, ErrorMessage = "{0} must be between 1 and 50 characters")]
            public string Title { get; set; }

            [YearToDate(earliest: 1878, ErrorMessage = "No movies were produced in that year")]
            public int Year { get; set; }

            [Range(10, 240)]
            [Display(Name = "Runtime (minutes)")]
            public int Runtime { get; set; }

            [Range(0, 10)]
            [Display(Name = "IMDb Rating (0-10)")]
            public double ImdbRating { get; set; }

            [Range(0, 100)]
            [Display(Name = "Rotten Tomato Score (%)")]
            public int RottenTomatoRating { get; set; }

            public List<MovieCast> Cast { get; set; }
            
            public class MovieCast
            {
                public int ActorId { get; set; }
                public string CharacterName { get; set; }
            }

            /// <summary>
            /// Initializes a new command view model for creating and editing movie objects
            /// </summary>
            public Command()
            {
                Cast = new List<MovieCast>();
            }
        }

        /// <summary>
        /// Represents a command handler for creating and editing movie records
        /// </summary>
        public class CommandHandler : IRequestHandler<Command, int>
        {
            private readonly ApplicationDbContext _db;

            public CommandHandler(ApplicationDbContext db)
            {
                _db = db;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var movie = request.Id == null
                    ? new Domain.Movie()
                    : await _db.Movies.SingleOrDefaultAsync(m => m.Id == request.Id);

                movie.MapFrom(request);
                await _db.Movies.AddAsync(movie);
                await _db.SaveChangesAsync();

                // Just make an API controller for movie cast
                //foreach (var cast in request.Cast)
                //{
                //    if(!_db.CastingCredits.Any(cc => cc.ActorId == cast.ActorId && cc.CharacterName == cast.CharacterName && cc.MovieId == movie.Id)){
                //        var movieCast = new Domain.CastingCredit();
                //        movieCast.MapFrom(cast, movie.Id);
                //        await _db.CastingCredits.AddAsync(movieCast);
                //        await _db.SaveChangesAsync();
                //    }
                //}

                return movie.Id;
            }
        }
    }
}
