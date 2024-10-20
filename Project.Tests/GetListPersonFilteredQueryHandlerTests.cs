﻿

using AutoMapper;
using Moq;
using Project.Application.DTOs.Person;
using Project.Application.Interfaces.Repositories;
using Project.Application.Mapping;
using Project.Application.Queries.Person;
using Project.Domain.Entities;
using System.Linq.Expressions;

namespace Project.Tests
{
	public class GetListPersonFilteredQueryHandlerTests
	{
		private readonly Mock<IPersonRepository> _personRepositoryMock;
		private readonly IMapper _mapper;
		private readonly GetListPersonFilteredQueryHandler _handler;

		public GetListPersonFilteredQueryHandlerTests()
		{
			// Configurar el mock del repositorio
			_personRepositoryMock = new Mock<IPersonRepository>();

			// Configurar el mapeador (AutoMapper)
			var mockMapperConfig = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new MappingProfile()); 
			});
			_mapper = mockMapperConfig.CreateMapper();

			// Instanciar el handler con el repositorio y el mapeador
			_handler = new GetListPersonFilteredQueryHandler(_personRepositoryMock.Object, _mapper);
		}

		[Fact]
		public async Task Handle_ShouldReturnFilteredPersons_WhenValidFilterIsProvided()
		{
			// Arrange: configurar datos de ejemplo para la prueba
			var filter = new PersonListRequestDto
			{
				FirstName = "Peter",
				Email = "pparker@string.com"
			};

			var personList = new List<Person>
		{
			new Person { Name = "Peter", LastName = "Parker", Email = "pparker@string.com" },
			new Person { Name = "Tony", LastName = "Stark", Email = "tstark@string.com" }
		};

			// Configurar el mock para que devuelva la lista de personas filtradas
			_personRepositoryMock
				.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Person, bool>>>()))
				.ReturnsAsync((Expression<Func<Person, bool>> filterExpression) =>
				{
					return personList.AsQueryable().Where(filterExpression).ToList();
				});

			var query = new GetListPersonFilteredQuery(filter);

			// Act: ejecutar el handler
			var result = await _handler.Handle(query, CancellationToken.None);

			// Assert: verificar que el resultado es correcto
			Assert.NotNull(result);
			Assert.Single(result); // Debería devolver solo una persona
			Assert.Equal("Peter", result.First().Name);
		}
	}

}
