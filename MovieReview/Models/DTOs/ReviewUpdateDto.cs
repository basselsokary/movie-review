namespace MovieReview.Models.DTOs;

public class ReviewUpdateDto
{
    public int Id { get; set; }

    public int Rate { get; set; }

    public string? Comment { get; set; }

    public int MovieId { get; set; }

    public string UserId { get; set; }
}
