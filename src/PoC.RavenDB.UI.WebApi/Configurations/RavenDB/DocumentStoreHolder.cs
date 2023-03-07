using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.Exceptions;
using Raven.Client.Exceptions.Database;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace PoC.RavenDB.UI.WebApi.Configurations.RavenDB;

public class DocumentStoreHolder
{
    private static readonly Lazy<IDocumentStore> _store = new(CreateStore);

    public static IDocumentStore Store => _store.Value;

    private static IDocumentStore CreateStore()
    {
        var store = new DocumentStore
        {
            Urls = new[] { "http://localhost:8088/" },
            Database = "Cine"
        }.Initialize();

        EnsureDatabaseExists(store);

        return store;
    }

    private static void EnsureDatabaseExists(IDocumentStore store,
        string database = null,
        bool createDatabaseIfNotExists = true)
    {
        database = database ?? store.Database;

        if (string.IsNullOrWhiteSpace(database))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(database));
        }

        try
        {
            store.Maintenance.ForDatabase(database).Send(new GetStatisticsOperation());
        }
        catch (DatabaseDoesNotExistException)
        {
            if (createDatabaseIfNotExists == false)
            {
                throw;
            }

            try
            {
                store.Maintenance.Server.Send(new CreateDatabaseOperation(new DatabaseRecord(database)));
            }
            catch (ConcurrencyException)
            {
                // The database was already created before calling CreateDatabaseOperation
            }
        }
    }
}