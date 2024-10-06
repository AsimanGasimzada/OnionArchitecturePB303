using Microsoft.EntityFrameworkCore;
using Pustok.Core.Entities.Common;
using Pustok.DataAccess.Contexts;
using Pustok.DataAccess.Repositories.Abstractions.Generic;
using System.Linq.Expressions;

namespace Pustok.DataAccess.Repositories.Implementations.Generic;

internal class Repository<T> : IRepository<T> where T : BaseEntity, new()
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public IQueryable<T> GetAll(params string[] includes)
    {
      var query= _context.Set<T>().AsQueryable();

        foreach (var include in includes)
        {
            query.Include(include);
        }

        return query;
    }

    public Task<T?> GetAsync(Expression<Func<T, bool>> expression, params string[] includes)
    {
        var query = GetAll(includes);


        var entity=query.FirstOrDefaultAsync(expression);

        return entity;
    }

    public IQueryable<T> GetFilter(Expression<Func<T, bool>> expression, params string[] includes)
    {
        var query = _context.Set<T>().Where(expression);

        foreach (var include in includes)
        {
            query.Include(include);
        }

        return query;
    }

    public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression)
    {
      bool isExist=await _context.Set<T>().AnyAsync(expression);

        return isExist;
    }

    public async Task<int> SaveChangesAsync()
    {
       return await _context.SaveChangesAsync();
    }

    public void Update(T entity)
    {
      _context.Set<T>().Update(entity);
    }
}
