using System.ComponentModel.DataAnnotations;

namespace MovieReview.Models.DTOs;

public class WatchlistUpdateDto
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string? Desription { get; set; }
}
