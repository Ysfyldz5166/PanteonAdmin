using Microsoft.EntityFrameworkCore;
using Panteon.DataAcces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Panteon.DataAcces.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbContext _context;
        protected internal readonly DbSet<T> DbSet;
        protected readonly IUnitOfWork _uow;

        public GenericRepository(IUnitOfWork uow)
        {
            _context = uow.Context;
            _uow = uow;
            DbSet = _context.Set<T>();
        }

        public IQueryable<T> All => _context.Set<T>();

        public void Add(T entity)
        {
            _context.Add(entity);
        }

        public IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            return GetAllIncluding(includeProperties);
        }

        public IQueryable<T> FindByInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = GetAllIncluding(includeProperties);
            return query.Where(predicate);
        }

        public async Task<T> FindByFirstAsync(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> queryable = DbSet.AsNoTracking();
            return await queryable.FirstOrDefaultAsync(predicate);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> queryable = DbSet.AsNoTracking();
            return queryable.Where(predicate);
        }

        private IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = DbSet.AsNoTracking();
            return includeProperties.Aggregate(queryable, (current, includeProperty) => current.Include(includeProperty));
        }

        public T Find(Guid id)
        {
            return _context.Set<T>().Find(id);
        }

        public T FindByInt(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T> FindAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual void Update(T entity)
        {
            _context.Update(entity);
        }

        public virtual void UpdateRange(List<T> entities)
        {
            _context.UpdateRange(entities);
        }

        public void RemoveRange(IEnumerable<T> lstEntities)
        {
            _context.Set<T>().RemoveRange(lstEntities);
        }

        public void AddRange(IEnumerable<T> lstEntities)
        {
            _context.Set<T>().AddRange(lstEntities);
        }

        public void InsertUpdateGraph(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public virtual void Delete(Guid id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity != null)
            {
                _context.Remove(entity);
            }
        }

        public virtual void Delete(T entity)
        {
            _context.Remove(entity);
        }

        public virtual void Remove(T entity)
        {
            _context.Remove(entity);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
