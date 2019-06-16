using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LevelApp.DAL.Models.Base;
using LevelApp.DAL.Repositories.Base;

namespace LevelApp.BLL.Services.Base
{
    public class BaseService<TEntity, TKey>: IBaseService<TEntity, TKey> where TEntity : Entity<TKey>
    {
        public IBaseRepository<TEntity, TKey> Repository { get; }

        public TEntity Get(TKey id)
        {
            return Repository.Get(id);
        }

        public IList<TEntity> GetAll()
        {
            return Repository.GetAll();
        }

        public TEntity Create(TEntity entity)
        {
            Repository.Insert(entity);
            Repository.Save();

            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            Repository.Update(entity);
            Repository.Save();

            return entity;
        }

        public TEntity Remove(TEntity entity)
        {
            Repository.Delete(entity);
            Repository.Save();

            return entity;
        }
    }
}
