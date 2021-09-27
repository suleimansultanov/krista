using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KristaShop.DataReadOnly.Interfaces
{
    public interface IReadOnlyRepo<T>
        where T : class
    {
        IQueryable<T> QueryReadOnly();

        IQueryable<T> QueryFindBy(Expression<Func<T, bool>> predicate);

        T FindById(Guid id);

        Task<T> FindByIdAsync(Guid id);

        Task<T> FindByFilterAsync(Expression<Func<T, bool>> predicate);
    }
}
