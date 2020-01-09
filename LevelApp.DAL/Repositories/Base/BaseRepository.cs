using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using LevelApp.Crosscutting.Exceptions;
using LevelApp.Crosscutting.Helpers.PaginatedList;
using LevelApp.DAL.Models.Base;
using Z.EntityFramework.Plus;

namespace LevelApp.DAL.Repositories.Base
{
    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : Entity<TKey>
    {
        private DbSet<TEntity> _entities;

        protected DbSet<TEntity> Entities => _entities ?? (_entities = Context.Set<TEntity>());
        protected readonly DbContext Context;

        public BaseRepository(DbContext context)
        {
            Context = context;
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

        public async Task<IPaginatedList<TEntity>> GetPaginatedAsync(int pageIndex, int pageSize,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = Entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            var entities = await (orderBy != null ? orderBy(query).ToListAsync() : query.ToListAsync());
            var count = await Entities.CountAsync();

            return new PaginatedList<TEntity>(entities, count, pageIndex, pageSize);
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

        public bool CheckIfAllExistByIds(IEnumerable<TKey> idList)
        {
            var validIds = Entities.Select(x => x.Id).ToList();
            var ids = idList.ToList();
            
            return validIds.Any() && ids.Any() && ids.All(x => validIds.Contains(x));
        }

        public async Task<bool> CheckIfAllExistByIdsAsync(IEnumerable<TKey> idList)
        {
            var validIds = await Entities.Select(x => x.Id).ToListAsync();
            var ids = idList.ToList();
            
            return validIds.Any() && ids.Any() && ids.All(x => validIds.Contains(x));
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

        public TKey InsertEntity<TAnyEntity>(TAnyEntity entity) where TAnyEntity : Entity<TKey>
        {
            Context.Add(entity);
            return entity.Id;
        }
        
        public void InsertEntityBatch<TAnyEntity>(IEnumerable<TAnyEntity> entities) where TAnyEntity : Entity<TKey>
        {
            Context.AddRange(entities);
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

        public TKey Delete(TKey id)
        {
            var entity = Get(id);

            if (entity == null)
            {
                throw new NotFoundException($"Entity of type {typeof(TEntity)} has not been found.", HttpStatusCode.NotFound);
            }
            return Delete(entity);
        }

        public TKey Delete(TEntity entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
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
            Context.SaveChanges();
        }

        public async void SaveAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
