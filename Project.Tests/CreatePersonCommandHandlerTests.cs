using Moq;
using Project.Application.Commands.Person;
using Project.Application.Interfaces.Repositories;
using Project.Application.DTOs.Person;
using Project.Domain.Entities;
using FluentAssertions;
using AutoMapper;

namespace Project.Tests
{
	public class CreatePersonCommandHandlerTests
	{

		private readonly Mock<IPersonRepository> _personRepositoryMock;
		private readonly CreatePersonCommandHandler _handler;
		private readonly Mock<IMapper> _mapperMock;

		public CreatePersonCommandHandlerTests()
		{
			_personRepositoryMock = new Mock<IPersonRepository>();
			_mapperMock = new Mock<IMapper>();

			_handler = new CreatePersonCommandHandler(_personRepositoryMock.Object, _mapperMock.Object);
		}


		[Fact]
		public async Task Handler_CreatePerson()
		{
			// Arrange
			var personRequestDto = new PersonRequestDto
			{
				Name = "Peter",
				LastName = "Parker",
				Email = "PeterParker@string.com",
				BirthDate = new DateTime(2000, 1, 1),
				PhoneNumber = "555599933"
			};

			var person = new Person
			{
				Id = 1,
				Name = "Peter",
				LastName = "Parker",
				Email = "PeterParker@string.com",
				BirthDate = new DateTime(2000, 1, 1),
				PhoneNumber = "555599933"
			};

			// Mapper: Dto to Entity
			_mapperMock.Setup(m => m.Map<Person>(It.IsAny<PersonRequestDto>()))
				.Returns(person);

			// repository return created person
			_personRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Person>()))
				.ReturnsAsync(person);

			var createPersonCommand = new CreatePersonCommand(personRequestDto);

			// Act: Execute method
			var result = await _handler.Handle(createPersonCommand, CancellationToken.None);

			// Assert: Verify that the result is as expected
			result.Should().Be(person.Id);

			// Verify AddAsync method was called once time with any Person
			_personRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Person>()), Times.Once);
		}


	}
}