using MovieReview.Models.DTOs;
using MovieReview.Models.Entities;
using OnlineMarketplace.Data.Repository;

namespace MovieReview.Data.Services.Interfaces;

public interface IActorService : IEntityBaseRepository<Actor>
{
    Task<Actor> CreateActorAsync(ActorCreateDto dto);
    Task<Actor?> UpdateActorAsync(ActorUpdateDto dto);
    Task<Actor?> GetActorIncluded(int id);
}
