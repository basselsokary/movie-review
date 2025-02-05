using OnlineMarketplace.Data.Repository;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MovieReview.Models.Entities;

public class Actor : IEntityBase
{
    [Key]
    public int Id { get; set; }

    public string? ProfilePictureURL { get; set; }

    [Required]
    [StringLength(maximumLength: 100, MinimumLength = 3)]
    public string FullName { get; set; } = null!;

    public string? Bio { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string Nationality { get; set; } = null!;

    public virtual List<MovieActor> MovieActors { get; set; }
}
