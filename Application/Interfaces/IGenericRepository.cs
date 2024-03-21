using System.Linq.Expressions;

namespace Application.Interfaces
{
	public interface IGenericRepository<T>
	{
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);

        Task CreateAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}