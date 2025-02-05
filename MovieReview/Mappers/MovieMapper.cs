using MovieReview.Models.DTOs;
using MovieReview.Models.Entities;

namespace MovieReview.Mappers;

public static class MovieMapper
{
    public static Movie ToEntity(this MovieCreateDto dto) => new()
    {
        Name = dto.Name,
        Storyline = dto.Storyline,
        PosterURL = dto.PosterURL,
        ReleaseDate = dto.ReleaseDate
    };

    public static MovieReadDto ToReadDto(this Movie movie)
    {
        var movieRead = new MovieReadDto()
        {
            Id = movie.Id,
            Name = movie.Name,
            PosterURL = movie.PosterURL,
            ReleaseDate = movie.ReleaseDate,
            Storyline = movie.Storyline,
        };

        if (movie.MovieActors != null)
        {
            movieRead.Actors = movie.MovieActors?.Select(ma =>
                new MovieActorsDto
                {
                    ActorId = ma.ActorId,
                    ActorFullName = ma.Actor?.FullName ?? string.Empty,
                    ProfilePictureURL = ma.Actor?.ProfilePictureURL ?? string.Empty,
                    CharacterName = ma.CharacterName ?? string.Empty,
                }).ToList();
        }

        if (movie.Genres != null)
        {
            movieRead.Genres = movie.Genres.Select(ma => ma.Name).ToList();
        }

        return movieRead;
    }

    public static MovieCreateDto ToCreateDto(this Movie movie) => new()
    {
        Name = movie.Name,
        PosterURL = movie.PosterURL,
        ReleaseDate = movie.ReleaseDate,
        Storyline = movie.Storyline,
        ActorIds = movie.MovieActors.Select(ma => ma.ActorId).ToList(),
        GenreIds = movie.Genres.Select(ma => ma.Id).ToList()
    };
}
