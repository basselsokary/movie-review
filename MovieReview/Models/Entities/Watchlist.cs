using OnlineMarketplace.Data.Repository;
using System.ComponentModel.DataAnnotations;

namespace MovieReview.Models.Entities;

public class Watchlist : IEntityBase
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string? Desription { get; set; }

    public string UserId { get; set; }
    public virtual AppUser User { get; set; }

    public virtual List<Movie> Movies { get; set; } = null!;

}