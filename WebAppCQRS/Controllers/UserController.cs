
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Commands.User;
using Project.Application.DTOs.Login;
using Project.Application.DTOs.User;
using Project.Application.Services;

namespace WebAppCQRS.Controllers
{
    [Route("api/v1/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly JwtService _jwtService;
		private readonly IMediator _mediator;

		public UserController(JwtService jwtService, IMediator mediator)
		{
			_jwtService = jwtService;
			_mediator = mediator;
		}

		[HttpPost("Login")]
		public ActionResult Login([FromBody] LoginRequestDto sessionUser)
		{
			var token = _jwtService.GenerateJwtToken(sessionUser.Email);
			//var response = await securityService.ValidateLogin(loginUser);
			return Ok(token);
		}


		[HttpPost("create")]
		public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
		{
			var command = new CreateUserCommand(userDto);
			var result = await _mediator.Send(command);

			if (result)
				return Ok("Usuario creado con éxito");

			return BadRequest("Error al crear el usuario");
		}


	}
}
