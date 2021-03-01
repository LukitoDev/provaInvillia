using LocaGames.Domain.Interfaces.Repositories;
using LocaGames.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LocaGames.Service.Services
{
    public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> _repository;

        public ServiceBase(IRepositoryBase<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task Add(TEntity obj)
        {
            await _repository.Add(obj);
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _repository.GetAll();
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<TEntity> GetFirst(Expression<Func<TEntity, bool>> expression = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            return await _repository.GetFirst(expression, orderBy, includeProperties);
        }

        public async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> expression = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            return await _repository.GetList(expression, orderBy, includeProperties);
        }

        public async Task Remove(TEntity obj)
        {
            await _repository.Remove(obj);
        }

        public virtual async Task Update(TEntity obj)
        {
            await _repository.Update(obj);
        }

        public virtual async Task Remove(int id)
        {
            await _repository.Remove(id);
        }
    }
}