using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ScupTel.Domain.Core.Interfaces
{
    public interface IServiceBase<TEntity, TIdentifier> : IDisposable where TEntity : class
    {
        TEntity Get(TIdentifier id);
        TEntity Find(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> All();
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);
        TEntity Save(TEntity entity);
        bool Delete(TIdentifier id);
        bool Delete(TEntity entity);
    }
}
