using ScupTel.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ScupTel.Domain.Core.Models
{
    public class ServiceBase<TEntity, TIdentifier> : IServiceBase<TEntity, TIdentifier> where TEntity : Entity<TIdentifier>
    {
        private readonly IRepositoryBase<TEntity, TIdentifier> _repository;

        public ServiceBase(IRepositoryBase<TEntity, TIdentifier> repository)
        {
            _repository = repository;
        }

        public IEnumerable<TEntity> All()
        {
            return _repository.All();
        }

        public bool Delete(TIdentifier id)
        {
            return _repository.Delete(id);
        }

        public bool Delete(TEntity entity)
        {
            return _repository.Delete(entity);
        }

        public TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.Find(predicate);
        }

        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public TEntity Get(TIdentifier id)
        {
            return _repository.Get(id);
        }

        public TEntity Save(TEntity entity)
        {
            return _repository.Save(entity);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
