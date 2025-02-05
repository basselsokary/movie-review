using System.ComponentModel.DataAnnotations;

namespace MovieReview.Models.DTOs;

public class MovieReadDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string? Storyline { get; set; }

    public string? PosterURL { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public List<string>? Genres { get; set; }

    public List<MovieActorsDto>? Actors { get; set; }
}
