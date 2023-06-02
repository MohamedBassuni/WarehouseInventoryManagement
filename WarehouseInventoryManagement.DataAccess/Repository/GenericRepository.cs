
using WarehouseInventoryManagement.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseInventoryManagement.DataAccess.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : EntityBase
    {
        protected WarehouseInventoryManagementDBContext db;
        public GenericRepository(WarehouseInventoryManagementDBContext db)
        {
            this.db = db;
        }
        public async Task<TEntity> Add(TEntity entity)
        {
            await db.Set<TEntity>().AddAsync(entity);
            await db.SaveChangesAsync();
            return entity;
        }
        public virtual async Task<TEntity> Get(int id)
        {
            return await db.Set<TEntity>().FindAsync(id);
        }
        public virtual async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {

            return await db.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }
        public virtual async Task<List<TEntity>> GetAll()
        {
            return await db.Set<TEntity>().ToListAsync();
        }
        public async Task<bool> Update(TEntity entity)
        {
            db.Update<TEntity>(entity).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Remove(TEntity entity)
        {
            db.Set<TEntity>().Remove(entity);
            await db.SaveChangesAsync();
            return true;
        }
    }
}
