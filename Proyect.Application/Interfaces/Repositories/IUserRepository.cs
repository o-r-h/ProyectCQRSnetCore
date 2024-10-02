using Project.Domain.Entities;

namespace Project.Application.Interfaces.Repositories
{
	public interface IUserRepository
	{
		Task DeleteASync(long userId);
		Task<User> GetByEmail(string email);
		Task<long> Insert(User data);
		Task<User> SelectByIdASync(long userId);
		Task<User> UpdateASync(long userId, User obj);
	
	}
}