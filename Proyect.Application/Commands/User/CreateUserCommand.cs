using MediatR;
using Project.Application.DTOs.User;

namespace Project.Application.Commands.User
{
    public class CreateUserCommand : IRequest<bool>  // Se espera que el comando devuelva un valor booleano indicando éxito o fracaso
	{
		public CreateUserDto UserDto { get; set; }

		public CreateUserCommand(CreateUserDto createUserDto)
		{
			UserDto = createUserDto;
		}
	}
}
