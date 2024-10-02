using Project.Domain.Entities;
using Project.Application.Interfaces.Repositories;
using Project.Infrastructure.Persitence.Context;

namespace Project.Infrastructure.Persitence.Repositories
{
	public class PersonRepository : RepositoryBase<Person>, IPersonRepository
	{
		public PersonRepository(AppDbContext context) : base(context)
		{
		}
	}
}
