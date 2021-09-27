using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KristaShop.DataAccess.Interfaces
{
    public interface INomRepository<T> where T : class
    {
        void Add(T entity);
        void AddRange(List<T> entities);
        Task<int> CountAsync();
        IQueryable<T> Filter(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", int? page = null, int? pageSize = null);
        T FindByFilter(Expression<Func<T, bool>> predicate);
        Task<T> FindByFilterAsync(Expression<Func<T, bool>> predicate);
        T FindById(Guid id);
        Task<T> FindByIdAsync(Guid id);
        Task<ICollection<T>> GetAllAsync();
        Task<ICollection<T>> GetAllFindByAsync(Expression<Func<T, bool>> predicate);
        bool IsExist(Expression<Func<T, bool>> predicate);
        IQueryable<T> Query();
        IQueryable<T> QueryFindBy(Expression<Func<T, bool>> predicate);
        void Remove(T entity);
        void RemoveRange(List<T> entities);
        Task<int> SaveChangesAsync();
        void Update(T updated);
        void DetachEntity(T updated);
        void UpdateRange(List<T> entities);
    }
}