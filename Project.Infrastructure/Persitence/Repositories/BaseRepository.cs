using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities;
using Project.Infrastructure.Persitence.Context;
using System.Linq.Expressions;
using Project.Application.Interfaces;


namespace Project.Infrastructure.Persitence.Repositories
{
	public class RepositoryBase<T> : IAsyncRepository<T> where T : BaseEntity
	{
		protected readonly AppDbContext _context;

		public RepositoryBase(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IReadOnlyList<T>> GetAllAsync()
		{
			return await _context.Set<T>().ToListAsync();
		}

		public async Task<IQueryable<T>> GetAllIQuerableAsync()
		{

			var xx = await _context.Set<T>().ToListAsync();
			var yy = xx.AsQueryable();
			return yy;
		}


		public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
		{
			return await _context.Set<T>().Where(predicate).ToListAsync();
		}

		public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
									   Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
									   string includeString = null,
									   bool disableTracking = true)
		{
			if (string.IsNullOrEmpty(includeString))
			{
				throw new ArgumentException($"'{nameof(includeString)}' cannot be null or empty.", nameof(includeString));
			}

			IQueryable<T> query = _context.Set<T>();
			if (disableTracking) query = query.AsNoTracking();

			if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

			if (predicate != null) query = query.Where(predicate);

			if (orderBy != null)
				return await orderBy(query).ToListAsync();


			return await query.ToListAsync();
		}

		public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
									 Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
									 List<Expression<Func<T, object>>> includes = null,
									 bool disableTracking = true)
		{

			IQueryable<T> query = _context.Set<T>();
			if (disableTracking) query = query.AsNoTracking();

			if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

			if (predicate != null) query = query.Where(predicate);

			if (orderBy != null)
				return await orderBy(query).ToListAsync();


			return await query.ToListAsync();
		}



		public async Task<IReadOnlyList<T>> GetAsync(
	Expression<Func<T, bool>> predicate = null,
	Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
	List<Expression<Func<T, object>>> includes = null,
	bool disableTracking = true,
	int? skip = null,
	int? take = null)
		{
			IQueryable<T> query = _context.Set<T>();
			if (disableTracking) query = query.AsNoTracking();

			if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

			if (predicate != null) query = query.Where(predicate);

			if (orderBy != null) query = orderBy(query);

			if (skip.HasValue) query = query.Skip(skip.Value);
			if (take.HasValue) query = query.Take(take.Value);

			return await query.ToListAsync();
		}


		public async Task<T> AddAsync(T entity)
		{
			_context.Set<T>().Add(entity);
			try
			{
				_context.Set<T>().Add(entity);
				await _context.SaveChangesAsync();
				return entity;
			}
			catch (Exception ex)
			{

				throw ex;
			}

		}

		public async Task<T> UpdateAsync(T entity)
		{
			_context.Set<T>().Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return entity;
		}

		public async Task DeleteAsync(T entity)
		{
			_context.Set<T>().Remove(entity);
			await _context.SaveChangesAsync();
		}

		public async Task<long?> DeleteByIdAsync(long id)
		{
			var entity = await _context.Set<T>().FindAsync(id);
			if (entity == null) return null;
			_context.Set<T>().Remove(await GetByIdAsync(id));
			return await _context.SaveChangesAsync();
		}

		public virtual async Task<T> GetByIdAsync(long id)
		{
			var result = await _context.Set<T>().FindAsync(id);
			return result;

		}


		public void AddEntity(T entity)
		{
			_context.Set<T>().Add(entity);
		}

		public void UpdateEntity(T entity)
		{
			_context.Set<T>().Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
		}

		public void DeleteEntity(T entity)
		{
			_context.Set<T>().Remove(entity);
		}
	}
}
