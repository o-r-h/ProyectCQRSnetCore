using MediatR;
using Project.Application.DTOs.Person;

namespace Project.Application.Commands.Person
{
	public class CreatePersonCommand : IRequest<long>
	{
		public PersonRequestDto Person { get; set; }

		public CreatePersonCommand(PersonRequestDto person)
		{
			Person = person;
		}
	}
}
