using Microsoft.EntityFrameworkCore.Storage;
using PCI.Domain.Common;

namespace PCI.Application.Repositories;

public interface IUnitOfWork
{
    IIdentityRepository IdentityRepository { get; }
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

    Task<IDbContextTransaction> BeginTransactionAsync();
    Task<int> SaveChangesAsync();

}
