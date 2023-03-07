using PoC.RavenDB.Domain.Models;

namespace PoC.RavenDB.Domain.Interfaces.Database.Repositories;

public interface IMovieRepository
{
    Task Create(Movie movie);
    Task<Movie> ReadById(string id);
    Task<IEnumerable<Movie>> ReadByName(string name);
    Task<IEnumerable<Movie>> ReadAll();
    Task Update(Movie movie);
    Task Delete(string id);
}