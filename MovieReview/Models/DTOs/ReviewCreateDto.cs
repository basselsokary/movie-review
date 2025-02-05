namespace MovieReview.Models.DTOs;

public class ReviewCreateDto
{
    public int Rate { get; set; }

    public string? Comment { get; set; }

    public int MovieId { get; set; }

    public string UserId { get; set; }
}
