using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;

namespace ToDoApp.DAL.Repositories.Interfaces.Base;

public interface IBaseRepository<T>
    where T : class
{
    IQueryable<T> FindAll(Expression<Func<T, bool>>? predicate = default,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default);
    
    T Create(T entity);
    
    Task<T> CreateAsync(T entity);
    
    EntityEntry<T> Update(T entity);
    
    public void Delete(T entity);
    
    Task<IEnumerable<T?>> GetAllAsync(Expression<Func<T, bool>>? predicate = default,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default);

    Task<T?> GetFirstOrDefaultAsync(
        Expression<Func<T, bool>>? predicate = default,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default);
}