using Project.Domain.Entities;

namespace Project.Application.Interfaces.Repositories
{
	public interface IUserRepository
	{
		Task<User> ChangeUserStatusASync(long userId, long userStatusId);
		Task DeleteASync(long userId);
		Task<User> GetByEmail(string email);
		Task<User> GetByPasswordRecoveryToken(Guid token);
		Task<long> Insert(User data);
		Task<User> SelectByIdASync(long userId);
		Task<User> UpdateASync(long userId, User obj);
		Task UpdatePasswordRecoveryToken(long userId, Guid token);
	}
}