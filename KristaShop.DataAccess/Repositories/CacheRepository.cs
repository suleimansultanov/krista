using KristaShop.DataAccess.Domain;
using KristaShop.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KristaShop.DataAccess.Repositories
{
    public class CacheRepository<T> : ICacheRepository<T>
        where T : class
    {
        private readonly KristaShopDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly string classNameCache;

        public CacheRepository(KristaShopDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
            classNameCache = typeof(T).Name + "-key";
        }

        public IQueryable<T> Query()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public IQueryable<T> QueryFindBy(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            if (_cache.TryGetValue(classNameCache, out ICollection<T> list))
                return list;

            list = await _context.Set<T>().ToListAsync();
            _cache.Set(classNameCache, list);
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<ICollection<T>> GetAllFindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public T FindById(Guid id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T> FindByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public T FindByFilter(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).SingleOrDefault();
        }

        public async Task<T> FindByFilterAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).SingleOrDefaultAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();

            _cache.Remove(classNameCache);
            return entity;
        }

        public async Task<T> UpdateAsync(T updated)
        {
            _context.Set<T>().Attach(updated);
            _context.Entry(updated).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            _cache.Remove(classNameCache);
            return updated;
        }

        public async Task RemoveAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();

            _cache.Remove(classNameCache);
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public IQueryable<T> Filter(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", int? page = null,
            int? pageSize = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty);
            }

            if (page != null && pageSize != null)
                query = query.Skip(page.Value * pageSize.Value).Take(pageSize.Value);

            return query;
        }

        public bool IsExist(Expression<Func<T, bool>> predicate)
        {
            var exist = _context.Set<T>().Where(predicate);
            return exist.Any() ? true : false;
        }
    }
}
