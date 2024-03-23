using ESourcing.Core.Repositories.Base;
using ESourcing.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ESourcing.Infrastructure.Repository.Base
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        protected readonly WebAppContext _webAppContext;

        public Repository(WebAppContext webAppContext)
        {
            _webAppContext = webAppContext;
        }

        public async Task<T> AddAsync(T entity)
        {
          _webAppContext.Set<T>().Add(entity);
            await _webAppContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _webAppContext.Set<T>().Remove(entity);
            await _webAppContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _webAppContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _webAppContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, string includeString = null, bool disableTracking = true)
        {

            IQueryable<T> query = _webAppContext.Set<T>();
            if (string.IsNullOrWhiteSpace(includeString))
                query = query.Include(includeString);
            if (predicate is not null)
                query = query.Where(predicate);
            if (orderby is not null)
                return await orderby(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = _webAppContext.Set<T>();
            if (disableTracking)
                query = query.AsNoTracking();
            if (includes is not null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            if (predicate is not null)
                query = query.Where(predicate);
            if (orderby is not null)
                return await orderby(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return  await _webAppContext.Set<T>().FindAsync(id);
           
        }

        public async Task UpdateAsync(T entity)
        {
            _webAppContext.Entry(entity).State = EntityState.Modified;
            await _webAppContext.SaveChangesAsync();
        }
    }
}
