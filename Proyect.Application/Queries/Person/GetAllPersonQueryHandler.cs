using AutoMapper;
using MediatR;
using Project.Application.DTOs.Person;
using Project.Application.Interfaces.Repositories;


namespace Project.Application.Queries.Person
{
	public class GetAllPersonQueryHandler : IRequestHandler<GetAllPersonQuery, List<PersonListDto>>
	{
		private readonly IPersonRepository _repository;
		private readonly IMapper _mapper;
		public GetAllPersonQueryHandler(IPersonRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<List<PersonListDto>> Handle(GetAllPersonQuery request, CancellationToken cancellationToken)
		{
			var persons = await _repository.GetAllAsync();
			return _mapper.Map<List<PersonListDto>>(persons);
		}
	}
}
