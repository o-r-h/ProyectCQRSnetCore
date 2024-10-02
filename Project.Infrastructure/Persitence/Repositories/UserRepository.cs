
using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities;
using Project.Infrastructure.Persitence.Context;
using Project.Application.Interfaces.Repositories;

namespace Project.Infrastructure.Persitence.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly AppDbContext _context;

		public UserRepository(AppDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Create new record for User
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// 
		public async Task<long> Insert(User data)
		{
			try
			{
				_context.Set<User>().Add(data);
				await _context.SaveChangesAsync();
				return data.UserId;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.ToString());
			}

		}

		/// <summary>
		/// Select by Id
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public async Task<User> SelectByIdASync(long userId)
		{
			User obj = await _context.Set<User>().FindAsync(userId);
			return obj;
		}

		/// <summary>
		/// Delete physical record User
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public async Task DeleteASync(long userId)
		{
			_context.Set<User>().Remove(await SelectByIdASync(userId));
			await _context.SaveChangesAsync();
		}


		/// <summary>
		/// Update User data
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="obj"></param>
		/// <returns></returns>
		public async Task<User> UpdateASync(long userId, User obj)
		{
			var objPrev = (from iClass in _context.Users where iClass.UserId == userId select iClass).FirstOrDefault();
			_context.Entry(objPrev).State = EntityState.Detached;
			_context.Entry(obj).State = EntityState.Modified;

			await _context.SaveChangesAsync();
			return obj;
		}


		public Task<User> GetByEmail(string email)
		{
			var objPrev = (from iClass in _context.Users where iClass.Email.ToLower() == email.ToLower() select iClass).FirstOrDefaultAsync();
			return objPrev;
		}

	




	}
}
