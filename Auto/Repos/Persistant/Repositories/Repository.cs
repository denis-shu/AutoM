using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using testAdmin.Core.Repositories;

namespace testAdmin.Persistant.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity: class
    {
        protected readonly DbContext Context;

        public Repository(DbContext _context)
        {
            Context = _context;
        }

        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
           return await Context.Set<TEntity>().ToListAsync();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> prodicate)
        {
            return Context.Set<TEntity>().Where(prodicate);
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
           // Context.Entry<TEntity>(entity).State = EntityState.Modified;

            Context.Set<TEntity>().AddOrUpdate(entity);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void Delete(TEntity entity)
        {
            Context.Entry<TEntity>(entity).State = EntityState.Deleted;
        } 
    }

    
}