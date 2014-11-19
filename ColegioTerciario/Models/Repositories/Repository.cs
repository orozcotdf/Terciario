using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ColegioTerciario.Models.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private ColegioTerciarioContext _db;
        public Repository()
        {
            _db = new ColegioTerciarioContext();
        }
        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _db.Set<TEntity>();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!String.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetById(object id)
        {
            return _db.Set<TEntity>().Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            _db.Set<TEntity>().Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = _db.Set<TEntity>().Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            _db.Attach<TEntity>(entityToDelete);
            _db.Set<TEntity>().Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _db.SetModified<TEntity>(entityToUpdate);
        }
    }
}