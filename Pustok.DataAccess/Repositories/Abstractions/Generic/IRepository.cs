using Pustok.Core.Entities.Common;
using System.Linq.Expressions;

namespace Pustok.DataAccess.Repositories.Abstractions.Generic;

public interface IRepository< T> where T : BaseEntity,new()
{
    IQueryable<T> GetAll(params string[] includes);
    IQueryable<T> GetFilter(Expression<Func<T,bool>> expression,params string[] includes);
    Task<T?> GetAsync(Expression<Func<T,bool>> expression,params string[] includes);
    Task<bool> IsExistAsync(Expression<Func<T, bool>> expression);

    Task CreateAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<int> SaveChangesAsync();

}
