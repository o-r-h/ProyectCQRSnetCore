using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Commands.Person;
using Project.Application.Commands.User;
using Project.Application.DTOs.Person;
using Project.Application.DTOs.User;
using Project.Application.Queries.Person;
using Project.Application.Services;

namespace WebAppCQRS.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class PersonController : ControllerBase
	{
		private readonly JwtService _jwtService;
		private readonly IMediator _mediator;

		public PersonController(JwtService jwtService, IMediator mediator)
		{
			_jwtService = jwtService;
			_mediator = mediator;
		}

		[HttpPost("create")]
		[Authorize]
		public async Task<IActionResult> CreatePerson([FromBody] PersonRequestDto personDto)
		{
			var command = new CreatePersonCommand(personDto);
			var result = await _mediator.Send(command);

			if (result !=0)
				return Ok($"Usuario {result} creado ");
			return BadRequest("Error al crear el usuario");
		}

		[HttpGet("GetAll")]
		///[Authorize]
		public async Task<IActionResult> GetAllPerson()
		{
			var command = new GetAllPersonQuery();
			var result = await _mediator.Send(command);
     		return Ok(result);
		}

	}
}
