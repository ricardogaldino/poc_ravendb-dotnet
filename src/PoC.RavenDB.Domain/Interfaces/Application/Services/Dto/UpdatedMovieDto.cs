namespace PoC.RavenDB.Domain.Interfaces.Application.Services.Dto;

public class UpdatedMovieDto
{
    public string Name { get; set; }
    public int Year { get; set; }
    public int Rating { get; set; }
    public string Genre { get; set; }
    public string Director { get; set; }
    public string Country { get; set; }
    public IEnumerable<string> Actors { get; set; }
    public IEnumerable<string> Languages { get; set; }
    public IEnumerable<string> Subtitles { get; set; }
}