using PoC.RavenDB.Application.Services;
using PoC.RavenDB.Domain.Interfaces.Application.Services;
using PoC.RavenDB.Domain.Interfaces.Database.Repositories;
using PoC.RavenDB.Infrastructure.Database.Repositories;
using PoC.RavenDB.UI.WebApi.Configurations.RavenDB;
using Raven.Client.Documents;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSingleton(DocumentStoreHolder.Store);
        
        builder.Services.AddScoped(serviceProvider => serviceProvider.GetService<IDocumentStore>()!.OpenAsyncSession());

        builder.Services.AddScoped<IMovieRepository, MovieRepository>();
        builder.Services.AddScoped<IMovieService, MovieService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}