using MovieReview.Models.DTOs;
using MovieReview.Models.Entities;
using OnlineMarketplace.Data.Repository;

namespace MovieReview.Data.Services.Interfaces;

public interface IMovieService : IEntityBaseRepository<Movie>
{
    Task<Movie> CreateMovieAsync(MovieCreateDto movieDto);
    Task<Movie?> UpdateMovieAsync(MovieUpdateDto movieDto);
    Task<Movie?> GetMovieIncluded(int id);
}
