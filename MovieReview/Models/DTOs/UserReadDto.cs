namespace MovieReview.Models.DTOs;

public class UserReadDto
{
    public string? FirstName { set; get; }

    public string? LastName { set; get; }

    public string? Country { get; set; }

    public string? ProfilePictureUrl { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public List<int>? ReviewIds { get; set; }

    public List<int>? WatchlistIds { get; set; }
}
