using LocaGames.Domain.Interfaces.Repositories;
using LocaGames.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LocaGames.Infra.Data.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly SqlContext _sqlContext;
        internal readonly IQueryable<TEntity> _dbSet;

        public RepositoryBase(SqlContext sqlContext)
        {
            _sqlContext = sqlContext;
            _dbSet = _sqlContext.Set<TEntity>().AsQueryable();
        }

        public async Task Add(TEntity obj)
        {
            _sqlContext.Set<TEntity>().Add(obj);

            await _sqlContext.SaveChangesAsync();
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await _sqlContext.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await _sqlContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetFirst(Expression<Func<TEntity, bool>> expression = null,
                                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).FirstOrDefaultAsync();
            }
            else
            {
                return await query.FirstOrDefaultAsync();
            }
        }

        public async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> expression = null,
                                                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                 string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async Task Remove(TEntity obj)
        {
            _sqlContext.Set<TEntity>().Remove(obj);

            await _sqlContext.SaveChangesAsync();
        }

        public virtual async Task Remove(int id)
        {
            if (id == 0)
            {
                return;
            }

            TEntity obj = _sqlContext.Set<TEntity>().Find(id);

            if (obj == null)
            {
                return;
            }

            _sqlContext.Set<TEntity>().Remove(obj);

            await _sqlContext.SaveChangesAsync();
        }

        public virtual async Task Update(TEntity obj)
        {
            _sqlContext.Entry(obj).State = EntityState.Modified;

            await _sqlContext.SaveChangesAsync();
        }

    }
}