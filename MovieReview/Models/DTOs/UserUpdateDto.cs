namespace MovieReview.Models.DTOs;

public class UserUpdateDto
{
    public string? FirstName { set; get; }

    public string? LastName { set; get; }

    public string? Country { get; set; }

    public string? ProfilePictureUrl { get; set; }
}
