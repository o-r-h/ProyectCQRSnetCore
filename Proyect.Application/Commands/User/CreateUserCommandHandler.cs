using MediatR;
using Project.Application.Interfaces.Repositories;
using Project.Application.Services;


namespace Project.Application.Commands.User
{
	public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
	{
		private readonly IUserRepository _userRepository;
		private readonly IPasswordHasher _passwordHasher;

		public CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
		{
			_userRepository = userRepository;
			_passwordHasher = passwordHasher;
		}

		public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
		{
			var dto = request.UserDto;
			// Encriptar la contraseña del usuario
			var hashedPassword = _passwordHasher.HashPassword(dto.Password);

			var user = new Domain.Entities.User
			{
				Email = dto.Email,
				Password = hashedPassword,
				FirstName = dto.FirstName,
				LastName = dto.LastName
			};

			// Guardar usuario en la base de datos
			await _userRepository.Insert(user);

			return true;
		}
	}
}
