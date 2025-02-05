using MovieReview.Models.DTOs;
using MovieReview.Models.Entities;

namespace MovieReview.Mappers;

public static class ActorMapper
{
    public static ActorReadDto ToReadDto(this Actor actor) => new()
    {
        Bio = actor.Bio,
        DateOfBirth = actor.DateOfBirth,
        FullName = actor.FullName,
        Nationality = actor.Nationality,
        ProfilePictureURL = actor.ProfilePictureURL,
        Movies = actor.MovieActors.Select(ma =>
            new ActorMoviesDto
            {
                MovieId = ma.MovieId,
                MovieName = ma.Movie.Name,
                PosterURL = ma.Movie.PosterURL
            }).ToList(),
    };
}
