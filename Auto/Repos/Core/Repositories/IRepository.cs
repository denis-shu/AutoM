using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace testAdmin.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity: class
    {
        TEntity Get(int id);
        Task<IEnumerable<TEntity>> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void Update(TEntity entity);

        void Remove(TEntity entity);
        void Delete(TEntity entity);
    }
}
