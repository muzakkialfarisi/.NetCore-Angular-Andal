using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Andal.DataAccess.Repositories.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task<IList<T>> ToListAsync(Expression<Func<T, bool>> filter = null,
                                                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                Func<IQueryable<T>, IIncludableQueryable<T, object>> includeProperties = null,
                                                bool disableTracking = true,
                                                bool ignoreQueryFilters = false);

        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> filter = null,
                                                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                Func<IQueryable<T>, IIncludableQueryable<T, object>> includeProperties = null,
                                                bool disableTracking = true,
                                                bool ignoreQueryFilters = false);

        Task AddAsync(T entity);

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
