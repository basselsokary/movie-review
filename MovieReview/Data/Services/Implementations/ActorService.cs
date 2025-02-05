using Microsoft.EntityFrameworkCore;
using MovieReview.Data.Repository.Implementations;
using MovieReview.Data.Services.Interfaces;
using MovieReview.Models.DTOs;
using MovieReview.Models.Entities;

namespace MovieReview.Data.Services.Implementations;

public class ActorService : EntityBaseRepository<Actor>, IActorService
{
    private readonly PlatformContext context;

    public ActorService(PlatformContext _context) : base(_context) => context = _context;

    public async Task<Actor> CreateActorAsync(ActorCreateDto dto)
    {
        var actor = new Actor
        {
            DateOfBirth = dto.DateOfBirth,
            Bio = dto.Bio,
            FullName = dto.FullName,
            Nationality = dto.Nationality,
            ProfilePictureURL = dto.ProfilePictureURL
        };

        await AddAsync(actor);
        await SaveAsync();

        var movieActors = dto.MovieIds.Select(movieId => new MovieActor
        {
            ActorId = actor.Id,
            MovieId = movieId
        });

        await context.MovieActors.AddRangeAsync(movieActors);
        await context.SaveChangesAsync();

        return actor;
    }

    public async Task<Actor?> UpdateActorAsync(ActorUpdateDto dto)
    {
        var actor = await GetByIdAsync(dto.Id, a => a.MovieActors);
        if (actor != null)
        {
            actor.FullName = dto.FullName;
            actor.Nationality = dto.Nationality;    
            actor.DateOfBirth = dto.DateOfBirth;
            actor.Bio = dto.Bio;
            actor.ProfilePictureURL = dto.ProfilePictureURL;

            // Remove old movies
            var moviesToRemoved = actor.MovieActors.Where(ma => !dto.MovieIds.Contains(ma.MovieId)).ToList();
            context.MovieActors.RemoveRange(moviesToRemoved);

            // Adding new movies
            var existingMovieIds = actor.MovieActors.Select(ma => ma.MovieId).ToList();
            var newMovieActors = dto.MovieIds.Where(movieId => !existingMovieIds.Contains(movieId))
                                          .Select(movieId => new MovieActor 
                                          { 
                                              ActorId = actor.Id,
                                              MovieId = movieId
                                          });

            await context.MovieActors.AddRangeAsync(newMovieActors);
            await context.SaveChangesAsync();

            return actor;
        }

        return null;
    }

    public async Task<Actor?> GetActorIncluded(int id)
        => await context.Actors.Include(a => a.MovieActors).ThenInclude(ma => ma.Movie).FirstOrDefaultAsync(a => a.Id == id);
}
