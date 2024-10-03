
using AutoMapper;
using MediatR;
using Project.Application.Interfaces.Repositories;

namespace Project.Application.Commands.Person
{
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, long>
    {
        private readonly IPersonRepository _personRepository;
		private readonly IMapper _mapper;

		public CreatePersonCommandHandler(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<long> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Person;
			var person = _mapper.Map<Domain.Entities.Person>(request.Person);
		    person = await _personRepository.AddAsync(person);
            if (person == null || person.Id == 0)
            {
                throw new ApplicationException("Failed to create person. The repository returned null or an invalid ID.");
            }
            return person.Id;
        }


    }
}
