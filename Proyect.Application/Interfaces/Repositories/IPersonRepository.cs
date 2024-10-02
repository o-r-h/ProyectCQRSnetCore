using Project.Domain.Entities;

namespace Project.Application.Interfaces.Repositories
{
	public interface IPersonRepository : IAsyncRepository<Person>
	{
	}
}
