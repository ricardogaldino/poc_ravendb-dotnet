using PoC.RavenDB.Domain.Interfaces.Application.Services.Dto;
using PoC.RavenDB.Domain.Models;

namespace PoC.RavenDB.Domain.Interfaces.Application.Services;

public interface IMovieService
{
    Task Create(Movie movie);
    Task<Movie> ReadById(string id);
    Task<IEnumerable<Movie>> ReadByName(string name);
    Task<IEnumerable<Movie>> Read(string? name);
    Task Update(string id, UpdatedMovieDto updatedMovie);
    Task Delete(string id);
}