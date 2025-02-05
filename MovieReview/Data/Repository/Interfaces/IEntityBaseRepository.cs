using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace OnlineMarketplace.Data.Repository;

public interface IEntityBaseRepository<T> where T: class, IEntityBase, new()
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filters, params Expression<Func<T, object>>[] includeProperties);
    Task<T?> GetByIdAsync(int id);
    Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includeProperties);
    Task<T> AddAsync(T entity);
    Task<T?> UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task SaveAsync();
}
