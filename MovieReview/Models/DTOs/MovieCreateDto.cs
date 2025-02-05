using System.ComponentModel.DataAnnotations;

namespace MovieReview.Models.DTOs;

public class MovieCreateDto
{
    [Required]
    public string Name { get; set; }

    public string? Storyline { get; set; }

    public string? PosterURL { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public List<int>? GenreIds { get; set; }

    public List<int>? ActorIds { get; set; }
}
