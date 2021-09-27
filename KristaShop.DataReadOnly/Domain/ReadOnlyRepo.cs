using KristaShop.DataReadOnly.Interfaces;
using KristaShop.DataReadOnly.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KristaShop.DataReadOnly.Domain
{
    public class ReadOnlyRepo<T> : IReadOnlyRepo<T>
        where T : class
    {
        private readonly KristaReplicaDbContext _context;

        public ReadOnlyRepo(KristaReplicaDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> QueryReadOnly()
        {
            return _context.Set<T>().AsNoTracking();
        }


        public IQueryable<T> QueryFindBy(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public T FindById(Guid id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T> FindByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> FindByFilterAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(predicate);
        }
    }
}
