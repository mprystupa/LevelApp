using LevelApp.DAL.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LevelApp.DAL.Repositories.Base
{
    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : Entity<TKey>
    {
        internal DbContext _context;
        internal DbSet<TEntity> _entities;

        private DbSet<TEntity> Entities => _entities ?? (_entities = _context.Set<TEntity>());

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public IList<TEntity> GetAll()
        {
            return Entities.AsNoTracking().ToList();
        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await Entities.AsNoTracking().ToListAsync();
        }

        public TEntity Get(TKey id)
        {
            return Entities.Find(id);
        }

        public async Task<TEntity> GetAsync(TKey id)
        {
            return await Entities.FindAsync(id);
        }

        public IList<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = Entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return orderBy != null ? orderBy(query).ToList() : query.ToList();
        }

        public async Task<IList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = Entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await (orderBy != null ? orderBy(query).ToListAsync() : query.ToListAsync());
        }

        public TEntity GetDetail(Func<TEntity, bool> predicate)
        {
            return Entities.FirstOrDefault(predicate);
        }

        public void Insert(TEntity entity)
        {
            Entities.Add(entity);
        }

        public void InsertBatch(IEnumerable<TEntity> entities)
        {
            Entities.AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            Entities.Attach(entity);
            Entities.Update(entity);
        }

        public void UpdateBatch(IEnumerable<TEntity> entities)
        {
            var enumerable = entities.ToList();

            Entities.AttachRange(enumerable);
            Entities.UpdateRange(enumerable);
        }

        public void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                Entities.Attach(entity);
            }
            Entities.Remove(entity);
        }

        public void DeleteBatch(IEnumerable<TEntity> entities)
        {
            Entities.RemoveRange(entities);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async void SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
