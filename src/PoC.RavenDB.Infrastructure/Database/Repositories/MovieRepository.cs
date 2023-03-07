using PoC.RavenDB.Domain.Interfaces.Database.Repositories;
using PoC.RavenDB.Domain.Models;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace PoC.RavenDB.Infrastructure.Database.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly IAsyncDocumentSession _session;

    public MovieRepository(IAsyncDocumentSession session)
    {
        _session = session;
    }

    public async Task Create(Movie movie)
    {
        await _session.StoreAsync(new List<Movie>());
        await _session.StoreAsync(movie);
        await _session.SaveChangesAsync();
    }

    public async Task<Movie> ReadById(string id)
    {
        return await _session.LoadAsync<Movie>(id);
    }

    public async Task<IEnumerable<Movie>> ReadByName(string name)
    {
        return await _session.Query<Movie>().
            Where(m => m.Name == name).
            ToListAsync();
    }

    public async Task<IEnumerable<Movie>> ReadAll()
    {
        return await _session.Query<Movie>().ToListAsync();
    }

    public async Task Update(Movie movie)
    {
        movie.UpdatedDate = DateTime.Now;
        await _session.SaveChangesAsync();
    }

    public async Task Delete(string id)
    {
        _session.Delete(id);
        await _session.SaveChangesAsync();
    }
}