using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LevelApp.DAL.Models.Base;
using LevelApp.DAL.Repositories.Base;
using LevelApp.DAL.UnitOfWork;

namespace LevelApp.BLL.Services.Base
{
    public interface IBaseService<TEntity, in TKey> where TEntity: Entity<TKey>
    {
        IBaseRepository<TEntity, TKey> Repository { get; }

        TEntity Get(TKey id);
        IList<TEntity> GetAll();
        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Remove(TEntity entity);
    }
}
