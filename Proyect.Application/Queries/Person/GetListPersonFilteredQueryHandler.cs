using AutoMapper;
using MediatR;
using Project.Application.DTOs.Person;
using Project.Application.Interfaces.Repositories;
using Project.Application.ValueObjects;
using Project.Domain.Entities;
using System.Linq.Expressions;

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

			Expression<Func<Domain.Entities.Person, bool>> predicate = p => true;

			if (!string.IsNullOrEmpty(request.Filter.NameAndLastName))
			{
				predicate = predicate.And(p => (p.Name + " " + p.LastName).Contains(request.Filter.NameAndLastName));
			}

			if (!string.IsNullOrEmpty(request.Filter.Email))
			{
				predicate = predicate.And(p => p.Email.Contains(request.Filter.Email));
			}

			var result = await _repository.GetAsync(predicate);
			return _mapper.Map<List<PersonListDto>>(result);
		}


	}
}
