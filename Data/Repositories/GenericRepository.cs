using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UnitOfWork.Data.Interfaces;

namespace UnitOfWork.Data.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected ApplicationDbContext context;
    internal DbSet<T> dbSet;
    public readonly ILogger _logger;

    public GenericRepository(ApplicationDbContext context, ILogger logger)
    {
        this.context = context;
        this.dbSet = context.Set<T>();
        _logger = logger;
    }

    public virtual async Task<T> GetById(Guid id)
    {
        return await dbSet.FindAsync(id);
    }

    public virtual async Task<bool> Add(T entity)
    {
        await dbSet.AddAsync(entity);
        return true;
    }

    public virtual Task<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public virtual Task<IEnumerable<T>> All()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
    {
        return await dbSet.Where(predicate).ToListAsync();
    }

    public virtual Task<bool> Update(T entity)
    {
        throw new NotImplementedException();
    }
}