using MediatR;
using Project.Application.DTOs.User;

namespace Project.Application.Commands.User
{
    public class CreateUserCommand : IRequest<bool>  
	{
		public CreateUserDto UserDto { get; set; }

		public CreateUserCommand(CreateUserDto createUserDto)
		{
			UserDto = createUserDto;
		}
	}
}
