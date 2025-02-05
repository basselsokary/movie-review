using Microsoft.EntityFrameworkCore;
using MovieReview.Data.Repository.Implementations;
using MovieReview.Data.Services.Interfaces;
using MovieReview.Models.DTOs;
using MovieReview.Models.Entities;

namespace MovieReview.Data.Services.Implementations;

public class MovieService : EntityBaseRepository<Movie>, IMovieService
{
    private readonly PlatformContext context;

    public MovieService(PlatformContext _context) : base(_context) => context = _context;

    public async Task<Movie> CreateMovieAsync(MovieCreateDto dto)
    {
        var movie = new Movie
        {
            Name = dto.Name,
            Storyline = dto.Storyline,
            PosterURL = dto.PosterURL,
            ReleaseDate = dto.ReleaseDate,
        };
        if (dto.GenreIds != null)
            movie.Genres = context.Genres.Where(g => dto.GenreIds.Contains(g.Id)).ToList();
        
        await context.Movies.AddAsync(movie);
        await context.SaveChangesAsync();
        
        if (dto.ActorIds != null)
        {
            var movieActors = dto.ActorIds.Select(actorId => new MovieActor
            {
                MovieId = movie.Id,
                ActorId = actorId
            }).ToList();

            await context.MovieActors.AddRangeAsync(movieActors); // Add directly to the join table
            await context.SaveChangesAsync();
        }

        return movie;
    }

    public async Task<Movie?> UpdateMovieAsync(MovieUpdateDto dto)
    {
        var movie = await GetByIdAsync(dto.Id, m => m.Genres, m => m.MovieActors);
        if (movie != null)
        {
            movie.Name = dto.Name;
            movie.Storyline = dto.Storyline;
            movie.PosterURL = dto.PosterURL;
            movie.ReleaseDate = dto.ReleaseDate;

            movie.Genres.Clear();
            movie.Genres = await context.Genres.Where(g => dto.GenreIds.Contains(g.Id)).ToListAsync();

            // Remove old actor relationships
            var actorsToRemove = movie.MovieActors.Where(ma => !dto.ActorIds.Contains(ma.ActorId)).ToList();
            context.MovieActors.RemoveRange(actorsToRemove);

            // Add new actor relationships
            var existingActorIds = movie.MovieActors.Select(ma => ma.ActorId).ToList();
            var newMovieActors = dto.ActorIds
                .Where(actorId => !existingActorIds.Contains(actorId))
                .Select(actorId => new MovieActor
                {
                    MovieId = movie.Id,
                    ActorId = actorId
                }).ToList();

            await context.MovieActors.AddRangeAsync(newMovieActors);
            await context.SaveChangesAsync();

            return movie;
        }

        return null;
    }

    public async Task<Movie?> GetMovieIncluded(int id)
        => await context.Movies.Include(m => m.MovieActors).ThenInclude(ma => ma.Actor).FirstOrDefaultAsync(m => m.Id == id);
}
