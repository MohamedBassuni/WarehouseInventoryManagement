using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WarehouseInventoryManagement.DataAccess.Entities;

namespace WarehouseInventoryManagement.DataAccess.Repository
{
    public interface IRepository<TEntity>
        where TEntity : EntityBase
    {
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Get(int Id);
        Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetAll();
        Task<bool> Update(TEntity entity); 
        Task<bool> Remove(TEntity entity);

    }
}
