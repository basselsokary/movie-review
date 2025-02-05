using MovieReview.Models.DTOs;
using MovieReview.Models.Entities;

namespace MovieReview.Mappers;

public static class ReviewMapper
{
    public static Review ToEntity(this ReviewCreateDto dto) => new()
    {
        Rate = dto.Rate,
        CreatedDate = DateTime.UtcNow,
        UpdatedDate = DateTime.UtcNow,
        Comment = dto.Comment,
        MovieId = dto.MovieId,
        UserId = dto.UserId
    };

    public static Review ToEntity(this ReviewUpdateDto dto, Review review) => new()
    {
        Id = dto.Id,
        Rate = dto.Rate,
        Comment = dto.Comment,
        CreatedDate = review.CreatedDate,
        UpdatedDate = DateTime.UtcNow,
        MovieId = dto.Id,
        UserId = dto.UserId
    };

    public static ReviewReadDto ToReadDto(this Review review) => new()
    {
        Comment = review.Comment,
        CreatedDate = review.CreatedDate,
        UpdatedDate = review.UpdatedDate,
        Rate = review.Rate,
        MovieId = review.MovieId,
        Movie = review.Movie.Name,
        UserName = review.User.UserName
    };
}
