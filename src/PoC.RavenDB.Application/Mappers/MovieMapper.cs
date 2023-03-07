using PoC.RavenDB.Domain.Interfaces.Application.Services.Dto;
using PoC.RavenDB.Domain.Models;

namespace PoC.RavenDB.Application.Mappers;

public static class MovieMapper
{
    public static void Map(UpdatedMovieDto updatedMovie, Movie movie)
    {
        movie.Name = updatedMovie.Name;
        movie.Year = updatedMovie.Year;
        movie.Rating = updatedMovie.Rating;
        movie.Genre = updatedMovie.Genre;
        movie.Director = updatedMovie.Director;
        movie.Country = updatedMovie.Country;
        movie.Actors = updatedMovie.Actors;
        movie.Languages = updatedMovie.Languages;
        movie.Subtitles = updatedMovie.Subtitles;
    }
}