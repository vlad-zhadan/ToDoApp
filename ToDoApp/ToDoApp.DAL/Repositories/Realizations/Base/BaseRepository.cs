using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using ToDoApp.DAL.Persistence;
using ToDoApp.DAL.Repositories.Interfaces.Base;

namespace ToDoApp.DAL.Repositories.Realizations.Base;

public abstract class BaseRepository<T> : IBaseRepository<T> 
    where T : class
{
    private readonly ToDoAppDbContext _dbContext;

    protected BaseRepository(ToDoAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public IQueryable<T> FindAll(Expression<Func<T, bool>>? predicate = default, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default)
    {
        return GetQueryable(predicate, include);
    }

    public T Create(T entity)
    {
        return _dbContext.Set<T>().Add(entity).Entity;
    }

    public async Task<T> CreateAsync(T entity)
    {
        var created = await _dbContext.Set<T>().AddAsync(entity);
        return created.Entity;
    }

    public EntityEntry<T> Update(T entity)
    {
        return _dbContext.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }

    public async Task<IEnumerable<T?>> GetAllAsync(Expression<Func<T, bool>>? predicate = default,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default)
    {
        return await GetQueryable(predicate, include).ToListAsync();
    }

    public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? predicate = default,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default)
    {
        return await GetQueryable(predicate, include).FirstOrDefaultAsync();
    }

    public IQueryable<T> GetQueryable(Expression<Func<T, bool>>? predicate = default,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default)
    {
        IQueryable<T> query = _dbContext.Set<T>().AsNoTracking();

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        if (include is not null)
        {
            query = include(query);
        }
        
        return query.AsNoTracking();
    }
}