using Microsoft.AspNetCore.Mvc;
using MovieReview.Data.Services.Interfaces;
using MovieReview.Models.DTOs;
using MovieReview.Models.Entities;
using MovieReview.Mappers;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using MovieReview.Data.Static;

namespace MovieReview.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IMovieService movieService;

    public MoviesController(IMovieService movieService) => this.movieService = movieService;

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int? genre)
    {
        IEnumerable<Movie> movies;
        if (genre == null)
            movies = await movieService.GetAllAsync(m => m.MovieActors, m => m.Genres);
        else
            movies = await movieService.GetAllAsync(filters: m => m.Genres.Any(g => g.Id == genre), m => m.MovieActors, m => m.Genres);

        if (movies == null)
            return NotFound();

        return Ok(movies.Select(m => m.ToReadDto()).ToList());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var movie = await movieService.GetMovieIncluded(id);
        if (movie == null)
            return NotFound();

        return Ok(movie.ToReadDto());
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<ActionResult> Create([FromBody] MovieCreateDto movieDto)
    {
        var movie = await movieService.CreateMovieAsync(movieDto);

        return CreatedAtAction(nameof(Get), new { id =  movie.Id }, movie.ToReadDto());
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> Update(int id, [FromBody] MovieUpdateDto movieDto)
    {
        if (movieDto.Id != id)
            return BadRequest();

        var movie = await movieService.UpdateMovieAsync(movieDto);
        if (movie == null)
            return NotFound();

        return Ok(movie.ToReadDto());
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> Delete(int id)
    {
        try {
            var movie = await movieService.GetByIdAsync(id);
            if (movie == null)
                return NotFound();

            await movieService.DeleteAsync(id);
            await movieService.SaveAsync();
            
            return NoContent();
        } catch {
            return BadRequest();
        }
    }
}
