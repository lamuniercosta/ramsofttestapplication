using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace RamSoftTaskApplication.Data.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _entities;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _entities.FindAsync(id).ConfigureAwait(false);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _entities.ToListAsync().ConfigureAwait(false);
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _entities.Where(predicate).ToListAsync().ConfigureAwait(false);
    }

    public async Task AddAsync(T entity)
    {
        await _entities.AddAsync(entity).ConfigureAwait(false);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _entities.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(T entity)
    {
        _entities.Remove(entity);
        await _context.SaveChangesAsync();
    }
}