using MediatR;
using Project.Application.DTOs.Person;


namespace Project.Application.Queries.Person
{
	public class GetAllPersonQuery : IRequest<List<PersonListDto>>
	{
	}
}
