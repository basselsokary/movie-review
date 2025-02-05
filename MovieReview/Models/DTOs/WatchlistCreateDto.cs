using System.ComponentModel.DataAnnotations;

namespace MovieReview.Models.DTOs;

public class WatchlistCreateDto
{
    [Required]
    public string Name { get; set; }

    public string? Desription { get; set; }
}
