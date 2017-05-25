using ScupTel.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ScupTel.Domain.Core.Interfaces
{
    public interface IRepositoryBase<TEntity, TIdentifier> : IDisposable where TEntity : Entity<TIdentifier>
    {
        TEntity Get(TIdentifier id);
        TEntity Find(Expression<Func<TEntity, bool>> predicate);
        ICollection<TEntity> All();
        ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);
        TEntity Save(TEntity entity);
        bool Delete(TIdentifier id);
        bool Delete(TEntity entity);
    }
}
