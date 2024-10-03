using MediatR;
using Project.Application.DTOs.Person;

namespace Project.Application.Queries.Person
{
	public class GetListPersonFilteredQuery : IRequest<List<PersonListDto>>
	{
		//set the filter
		public PersonListRequestDto Filter { get; }

		public GetListPersonFilteredQuery(PersonListRequestDto filter)
		{
			Filter = filter;
		}

	}
}
