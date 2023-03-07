namespace PoC.RavenDB.Domain.Models;

public class Movie
{
    private string _name = string.Empty;
    public string? Id { get; set; } = Guid.NewGuid().ToString();

    public string Name
    {
        get => _name.Trim();

        set => _name = value;
    }

    public int Year { get; set; }
    public int Rating { get; set; }
    public string Genre { get; set; }
    public string Director { get; set; }
    public string Country { get; set; }
    public IEnumerable<string> Actors { get; set; }
    public IEnumerable<string> Languages { get; set; }
    public IEnumerable<string> Subtitles { get; set; }
    
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? UpdatedDate { get; set; } = null;
}