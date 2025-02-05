using MovieReview.Models.Entities;
using OnlineMarketplace.Data.Repository;

namespace MovieReview.Data.Services.Interfaces;

public interface IReviewService : IEntityBaseRepository<Review>
{
}
