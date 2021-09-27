using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KristaShop.DataAccess.Interfaces
{
    public interface IShopRepository<T>
        where T : class
    {
        IQueryable<T> Query();

        IQueryable<T> QueryFindBy(Expression<Func<T, bool>> predicate);

        Task<ICollection<T>> GetAllAsync();

        Task<ICollection<T>> GetAllFindByAsync(Expression<Func<T, bool>> predicate);

        T FindById(Guid id);

        Task<T> FindByIdAsync(Guid id);

        T FindByFilter(Expression<Func<T, bool>> predicate);

        Task<T> FindByFilterAsync(Expression<Func<T, bool>> predicate);

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task RemoveAsync(T entity);

        Task<int> CountAsync();

        IQueryable<T> Filter(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            int? page = null,
            int? pageSize = null);

        bool IsExist(Expression<Func<T, bool>> predicate);
    }
}
