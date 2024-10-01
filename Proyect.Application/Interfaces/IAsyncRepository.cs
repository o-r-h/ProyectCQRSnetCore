using System.Linq.Expressions;
using Project.Domain.Entities;

namespace Project.Application.Interfaces
{
	public interface IAsyncRepository<T> where T : BaseEntity
	{
		Task<IReadOnlyList<T>> GetAllAsync();

		Task<IQueryable<T>> GetAllIQuerableAsync();

		Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);

		Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
									   Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
									   string includeString = null,
									   bool disableTracking = true);

		Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
									   Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
									   List<Expression<Func<T, object>>> includes = null,
									   bool disableTracking = true);

		Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
									   Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
									   List<Expression<Func<T, object>>> includes = null,
									   bool disableTracking = true,
									   int? skip = null,
									   int? take = null);

		Task<T> GetByIdAsync(long id);

		Task<T> AddAsync(T entity);

		Task<T> UpdateAsync(T entity);

		Task DeleteAsync(T entity);

		Task<long?> DeleteByIdAsync(long id);

		void AddEntity(T entity);

		void UpdateEntity(T entity);

		void DeleteEntity(T entity);
	}
}
