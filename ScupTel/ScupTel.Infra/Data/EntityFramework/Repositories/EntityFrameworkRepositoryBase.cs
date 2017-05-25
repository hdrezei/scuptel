using ScupTel.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using ScupTel.Domain.Core.Models;
using ScupTel.Infra.Data.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ScupTel.Infra.Data.EntityFramework.Repositories
{
    public class EntityFrameworkRepositoryBase<TEntity, TIdentifier> : IDisposable, IRepositoryBase<TEntity, TIdentifier> where TEntity : Entity<TIdentifier>
    {
        protected DbContext DbContext; 
        protected DbSet<TEntity> DbSet;

        public EntityFrameworkRepositoryBase(ScupTelDbContext context)
        {
            DbContext = context;
            DbSet = DbContext.Set<TEntity>();
        }
        
        public ICollection<TEntity> All()
        {
            return DbSet.AsTracking().ToList();
        }

        public bool Delete(TIdentifier id)
        {
            return Delete(Get(id));
        }

        public bool Delete(TEntity entity)
        {
            DbSet.Remove(entity);
            DbContext.SaveChanges();

            return true;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).AsTracking().SingleOrDefault();
        }

        public ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).AsTracking().ToList();
        }

        public TEntity Get(TIdentifier id)
        {
            return DbSet.Find(id);
        }

        public TEntity Save(TEntity entity)
        {
            DbSet.Add(entity);
            DbContext.SaveChanges();
            return entity;
        }
    }
}
