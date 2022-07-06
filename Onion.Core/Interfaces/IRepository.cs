using System.Linq;
using System.Linq.Expressions;
using Onion.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Onion.Core.Interfaces
{
    public interface IRepository<T> where T:BaseEntity
    {
        IQueryable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        T Find(Expression<Func<T, bool>> expression);
        Task<T> FindAsync(Expression<Func<T, bool>> expression);
        IQueryable<T> FindAll(Func<IQueryable<T>, IQueryable<T>> func);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<int> DeleteAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entityList);
        Task<IEnumerable<T>> DeleteRangeAsync(IEnumerable<T> entityList);
        Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entityList);
    }
}
