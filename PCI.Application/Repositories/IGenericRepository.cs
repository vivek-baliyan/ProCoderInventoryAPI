using PCI.Application.Specifications;
using PCI.Domain.Common;
using System.Linq.Expressions;

namespace PCI.Application.Repositories;

public interface IGenericRepository<T> where T : BaseEntity
{
    void Add(T entity);
    void Update(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entity);
    Task<IEnumerable<T>> GetAllAsync(string includeroperties = null);
    Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string includeroperties = null);
    Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>> filter, string includeroperties = null);
    Task<IEnumerable<T>> GetPaginatedAsync(int pageIndex, int pageSize, Expression<Func<T, bool>> filter = null,
        string includeroperties = null);
    Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
    Task<int> CountAsync();

    Task<IEnumerable<T>> GetAsync(ISpecification<T> specification);
    Task<T> GetFirstAsync(ISpecification<T> specification);
    Task<int> CountAsync(ISpecification<T> specification);
}
