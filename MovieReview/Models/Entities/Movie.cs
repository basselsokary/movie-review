using OnlineMarketplace.Data.Repository;
using System.ComponentModel.DataAnnotations;

namespace MovieReview.Models.Entities;

public class Movie : IEntityBase
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string? Storyline { get; set; }
    
    public string? PosterURL { get; set; }
    
    public DateTime? ReleaseDate { get; set; }
    
    public virtual List<Genres> Genres { get; set; }

    public virtual List<MovieActor> MovieActors { get; set; }
}
