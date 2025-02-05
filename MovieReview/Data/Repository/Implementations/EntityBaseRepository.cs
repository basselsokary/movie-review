using Microsoft.EntityFrameworkCore;
using OnlineMarketplace.Data.Repository;
using System.Linq.Expressions;
using System.Security.Principal;

namespace MovieReview.Data.Repository.Implementations;

public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
{
    private readonly PlatformContext context;
    private readonly DbSet<T> _table;

    public EntityBaseRepository(PlatformContext _context)
    {
        context = _context;
        _table = context.Set<T>();
    }

    public async Task<T> AddAsync(T entity)
    {
        await _table.AddAsync(entity);
        return entity;
    }

    public async Task<T?> UpdateAsync(T entity)
    {
        var existingEntity = await _table.FindAsync(entity.Id);

        if (existingEntity != null)
        {
            context.Entry(existingEntity).CurrentValues.SetValues(entity); // Update only the modified fields
            context.Entry(existingEntity).State = EntityState.Modified;  // Set state as Modified
            return existingEntity;
        }

        return null;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _table.FindAsync(id);
        if (entity != null)
        {
            _table.Remove(entity);  // Mark the entity for deletion
        }
    }

    public async Task<IEnumerable<T>> GetAllAsync() => await _table.ToListAsync();

    public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _table;
        query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        return await query.ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filters,
                                                  params Expression<Func<T, object>>[] includeProperties)
    {
        if (filters == null)
            throw new ArgumentNullException(nameof(filters));

        IQueryable<T> query = _table.Where(filters);

        if (includeProperties != null)
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        return await query.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id) => await _table.FindAsync(id);

    public async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _table;
        query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        return await query.FirstOrDefaultAsync(n => n.Id == id);
    }

    public async Task SaveAsync() => await context.SaveChangesAsync();
}