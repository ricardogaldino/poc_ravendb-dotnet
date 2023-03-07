using PoC.RavenDB.Application.Mappers;
using PoC.RavenDB.Domain.Interfaces.Application.Services;
using PoC.RavenDB.Domain.Interfaces.Application.Services.Dto;
using PoC.RavenDB.Domain.Interfaces.Database.Repositories;
using PoC.RavenDB.Domain.Models;

namespace PoC.RavenDB.Application.Services;

public class MovieService : IMovieService
{
    private const string AlreadyExistsError = "Movie already exists!";
    private const string DoesNotExistError = "Movie does not exist!";
    private const string DuplicateNameError = "Name used in another movie!";
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task Create(Movie movie)
    {
        var alreadyExists = await ReadById(movie.Id!);
        if (!string.IsNullOrWhiteSpace(alreadyExists?.Id))
        {
            throw new ArgumentException(AlreadyExistsError);
        }

        alreadyExists = (await ReadByName(movie.Name)).FirstOrDefault();
        if (!string.IsNullOrWhiteSpace(alreadyExists?.Name))
        {
            throw new ArgumentException(AlreadyExistsError);
        }

        await _movieRepository.Create(movie);
    }

    public async Task<Movie> ReadById(string id)
    {
        return await _movieRepository.ReadById(id);
    }

    public async Task<IEnumerable<Movie>> ReadByName(string name)
    {
        return await _movieRepository.ReadByName(name);
    }

    public async Task<IEnumerable<Movie>> Read(string? name)
    {
        IEnumerable<Movie> movies;

        if (string.IsNullOrWhiteSpace(name))
        {
            movies = await _movieRepository.ReadAll();
        }
        else
        {
            movies = await _movieRepository.ReadByName(name);
        }

        return movies;
    }

    public async Task Update(string id, UpdatedMovieDto updatedMovie)
    {
        var movie = await ReadById(id);
        if (string.IsNullOrWhiteSpace(movie?.Id))
        {
            throw new ArgumentException(DoesNotExistError);
        }

        var duplicateName = await ReadByName(updatedMovie.Name);
        if (duplicateName.Any(m => (m.Id != movie.Id) & (m.Name == updatedMovie.Name)))
        {
            throw new ArgumentException(DuplicateNameError);
        }

        MovieMapper.Map(updatedMovie, movie);

        await _movieRepository.Update(movie);
    }

    public async Task Delete(string id)
    {
        var movie = await ReadById(id);
        if (string.IsNullOrWhiteSpace(movie?.Id))
        {
            throw new ArgumentException(DoesNotExistError);
        }

        await _movieRepository.Delete(id);
    }
}