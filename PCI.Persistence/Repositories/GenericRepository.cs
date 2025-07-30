using Microsoft.EntityFrameworkCore;
using PCI.Application.Repositories;
using PCI.Application.Specifications;
using PCI.Domain.Common;
using PCI.Persistence.Context;
using System.Linq.Expressions;

namespace PCI.Persistence.Repositories;

public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context = context;
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public void Add(T entity)
    {
        _dbSet.Add(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void RemoveRange(T entity)
    {
        _dbSet.RemoveRange(entity);
    }

    public async Task<IEnumerable<T>> GetAllAsync(string includeroperties = null)
    {
        IQueryable<T> query = _dbSet;
        if (includeroperties != null)
        {
            foreach (var includeProp in includeroperties.Split([','], StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }
        return await query.ToListAsync();
    }

    public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string includeroperties = null)
    {
        IQueryable<T> query = _dbSet.Where(filter);
        if (includeroperties != null)
        {
            foreach (var includeProp in includeroperties.Split([','], StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }
        return await query.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>> filter, string includeroperties = null)
    {
        IQueryable<T> query = _dbSet.Where(filter);
        if (includeroperties != null)
        {
            foreach (var includeProp in includeroperties.Split([','], StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }
        return await query.ToListAsync();
    }

    public async Task<IEnumerable<T>> GetPaginatedAsync(
        int pageIndex,
        int pageSize,
        Expression<Func<T, bool>> filter = null,
        string includeroperties = null)
    {
        IQueryable<T> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includeroperties != null)
        {
            foreach (var includeProp in includeroperties.Split([','], StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }
        return await query
            .Take(pageSize)
            .Skip((pageIndex - 1) * pageSize)
            .ToListAsync();
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter)
    {
        return await _dbSet.AnyAsync(filter);
    }

    public async Task<int> CountAsync()
    {
        return await _dbSet.CountAsync();
    }

    public async Task<IEnumerable<T>> GetAsync(ISpecification<T> specification)
    {
        return await ApplySpecification(specification).ToListAsync();
    }

    public async Task<T> GetFirstAsync(ISpecification<T> specification)
    {
        return await ApplySpecification(specification).FirstOrDefaultAsync();
    }

    public async Task<int> CountAsync(ISpecification<T> specification)
    {
        return await ApplySpecification(specification).CountAsync();
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> specification)
    {
        var query = _dbSet.AsQueryable();

        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

        query = specification.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

        if (specification.OrderBy != null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if (specification.OrderByDescending != null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }

        if (specification.GroupBy != null)
        {
            query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
        }

        if (specification.IsPagingEnabled)
        {
            query = query.Skip(specification.Skip).Take(specification.Take);
        }

        return query;
    }
}
