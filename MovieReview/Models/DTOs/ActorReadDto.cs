using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MovieReview.Models.DTOs;

public class ActorReadDto
{
    [Required]
    [StringLength(maximumLength: 100, MinimumLength = 3)]
    public string FullName { get; set; } = null!;
    
    public string? ProfilePictureURL { get; set; }

    public string? Bio { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string Nationality { get; set; } = null!;

    public List<ActorMoviesDto>? Movies { get; set; }
}
