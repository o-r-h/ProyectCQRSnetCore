using Project.Domain.Entities;
using AutoMapper;
using Project.Application.DTOs.Person;

namespace Project.Application.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<PersonRequestDto, Person>();
			CreateMap<Person,PersonRequestDto>();
			CreateMap<Person, PersonListDto>();
		}
	}
}
