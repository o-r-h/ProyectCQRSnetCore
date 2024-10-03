using AutoMapper;
using MediatR;
using Project.Application.DTOs.Person;
using Project.Application.Interfaces.Repositories;

namespace Project.Application.Queries.Person
{
	public class GetListPersonFilteredQueryHandler : IRequestHandler<GetListPersonFilteredQuery, List<PersonListDto>>
	{
		private readonly IPersonRepository _repository;
		private readonly IMapper _mapper;

		public GetListPersonFilteredQueryHandler(IPersonRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<List<PersonListDto>> Handle(GetListPersonFilteredQuery request, CancellationToken cancellationToken)
		{
			// Obtener todas las personas desde el repositorio
			var persons = await _repository.GetAllAsync();

			// Aplicar filtros
			if (!string.IsNullOrEmpty(request.Filter.NameAndLastName))
			{
				persons = persons.Where(p =>
					(p.Name + " " + p.LastName).Contains(request.Filter.NameAndLastName, StringComparison.OrdinalIgnoreCase)).ToList();
			}

			if (!string.IsNullOrEmpty(request.Filter.Email))
			{
				persons = persons.Where(p => p.Email.Contains(request.Filter.Email, StringComparison.OrdinalIgnoreCase)).ToList();
			}

			// Mapear los resultados filtrados a DTOs
			return _mapper.Map<List<PersonListDto>>(persons);
		}


	}
}
