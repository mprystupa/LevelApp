using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using LevelApp.Crosscutting.Exceptions;
using LevelApp.DAL.Models.Base;

namespace LevelApp.DAL.Repositories.Base
{
    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : Entity<TKey>
    {
        private readonly DbContext _context;
        private DbSet<TEntity> _entities;

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
            var result = Entities.AsNoTracking().FirstOrDefault(predicate);

            if (result == null)
            {
                throw new NotFoundException($"Entity of type {typeof(TEntity)} has not been found.", HttpStatusCode.NotFound);
            }

            return result;
        }

        public async Task<TEntity> GetDetailAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var result = await Entities.AsNoTracking().FirstOrDefaultAsync(predicate, CancellationToken.None);
            
            if (result == null)
            {
                throw new NotFoundException($"Entity of type {typeof(TEntity)} has not been found.", HttpStatusCode.NotFound);
            }

            return result;
        }

        public bool CheckIfExists(Func<TEntity, bool> predicate)
        {
            return Entities.AsNoTracking().Any(predicate);
        }

        public async Task<bool> CheckIfExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Entities.AsNoTracking().AnyAsync(predicate);
        }

        public TKey Insert(TEntity entity)
        {
            Entities.Add(entity);
            return entity.Id;
        }

        public void InsertBatch(IEnumerable<TEntity> entities)
        {
            Entities.AddRange(entities);
        }

        public TKey Update(TEntity entity)
        {
            Entities.Attach(entity);
            return Entities.Update(entity).Entity.Id;
        }

        public void UpdateBatch(IEnumerable<TEntity> entities)
        {
            var enumerable = entities.ToList();

            Entities.AttachRange(enumerable);
            Entities.UpdateRange(enumerable);
        }

        public TKey Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                Entities.Attach(entity);
            }
            Entities.Remove(entity);

            return entity.Id;
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
