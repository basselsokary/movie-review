using MovieReview.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace MovieReview.Models.DTOs;

public class WatchlistReadDto
{
    public string Name { get; set; }

    public string? Desription { get; set; }

    public string UserName { get; set; }

    public List<int>? MovieIds { get; set; }
}
