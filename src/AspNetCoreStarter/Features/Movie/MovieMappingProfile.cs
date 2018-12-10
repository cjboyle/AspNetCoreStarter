using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace AspNetCoreStarter.Features.Movie
{
    public class MovieMappingProfile : Profile
    {
        public MovieMappingProfile()
        {
            CreateMap<Domain.Movie, CreateEdit.Command>();
            CreateMap<Domain.CastingCredit, CreateEdit.Command.MovieCast>();
        }
    }

    public static class MovieModelExtensions
    {
        public static void MapFrom(this Domain.Movie movie, CreateEdit.Command request)
        {
            movie.Id = request.Id ?? 0;
            movie.Title = request.Title;
            movie.Runtime = request.Runtime;
            movie.RottenTomatoRating = request.RottenTomatoRating;
            movie.ImdbRating = request.ImdbRating;
        }

        public static void MapFrom(this Domain.CastingCredit credit, CreateEdit.Command.MovieCast cast, int movieId)
        {
            credit.ActorId = cast.ActorId;
            credit.MovieId = movieId;
            credit.CharacterName = cast.CharacterName;
        }
    }
}
