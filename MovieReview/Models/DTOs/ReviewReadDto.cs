using MovieReview.Models.Entities;

namespace MovieReview.Models.DTOs;

public class ReviewReadDto
{
    public int Rate { get; set; }

    public string? Comment { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int MovieId { get; set; }
    public string Movie { get; set; }

    public string UserName { get; set; }
}
