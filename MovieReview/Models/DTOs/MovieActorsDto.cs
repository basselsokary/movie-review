namespace MovieReview.Models.DTOs;

public class MovieActorsDto
{
    public int ActorId { get; set; }

    public string ProfilePictureURL { get; set; } = null!;

    public string ActorFullName { get; set; } = null!;

    public string CharacterName { get; set; } = null!;
}
