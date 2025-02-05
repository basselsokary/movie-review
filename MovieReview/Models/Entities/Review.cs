using OnlineMarketplace.Data.Repository;

namespace MovieReview.Models.Entities;

public class Review : IEntityBase
{
    public int Id { get; set; }

    public int Rate { get; set; }

    public string? Comment { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int MovieId { get; set; }
    public virtual Movie Movie { get; set; }

    public string UserId { get; set; }
    public virtual AppUser User { get; set; }
}
