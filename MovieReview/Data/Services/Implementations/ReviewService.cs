using MovieReview.Data.Repository.Implementations;
using MovieReview.Models.Entities;

namespace MovieReview.Data.Services.Implementations;

public class ReviewService : EntityBaseRepository<Review>
{
    public ReviewService(PlatformContext _context) : base(_context)
    {
    }
}
