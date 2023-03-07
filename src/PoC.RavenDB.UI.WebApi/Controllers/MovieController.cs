using Microsoft.AspNetCore.Mvc;
using PoC.RavenDB.Domain.Interfaces.Application.Services;
using PoC.RavenDB.Domain.Interfaces.Application.Services.Dto;
using PoC.RavenDB.Domain.Models;

namespace PoC.RavenDB.UI.WebApi.Controllers;

[Route("movies")]
public class MovieController : Controller
{
    private const string UnexpectedError = "An unexpected error occurred, please contact support team!";
    private readonly ILogger<MovieController> _logger;
    private readonly IMovieService _movieService;

    public MovieController(
        ILogger<MovieController> logger,
        IMovieService movieService
    )
    {
        _logger = logger;
        _movieService = movieService;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Movie movie)
    {
        try
        {
            await _movieService.Create(movie);
            return StatusCode(StatusCodes.Status201Created, movie);
        }
        catch (ArgumentException ex)
        {
            return StatusCode(StatusCodes.Status422UnprocessableEntity, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, UnexpectedError);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById([FromRoute] string id)
    {
        try
        {
            var movie = await _movieService.ReadById(id);
            return StatusCode(StatusCodes.Status200OK, movie);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, UnexpectedError);
        }
    }

    [HttpGet]
    public async Task<ActionResult> Get([FromQuery] string? name)
    {
        try
        {
            var movies = await _movieService.Read(name);

            return StatusCode(StatusCodes.Status200OK, movies);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, UnexpectedError);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(
        [FromRoute] string id,
        [FromBody] UpdatedMovieDto updatedMovie)
    {
        try
        {
            await _movieService.Update(id, updatedMovie);
            return StatusCode(StatusCodes.Status200OK, updatedMovie);
        }
        catch (ArgumentException ex)
        {
            return StatusCode(StatusCodes.Status409Conflict, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, UnexpectedError);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] string id)
    {
        try
        {
            await _movieService.Delete(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        catch (ArgumentException ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, UnexpectedError);
        }
    }
}