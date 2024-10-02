using FluentValidation;

namespace Project.Application.Commands.Person
{
	public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
	{
		public CreatePersonCommandValidator()
		{
			RuleFor(x => x.Person.Name)
				.NotEmpty().WithMessage("The name is required.")
				.MaximumLength(45).WithMessage("The name cannot exceed 45 characters.");

			RuleFor(x => x.Person.LastName)
				.NotEmpty().WithMessage("The last name is required.")
				.MaximumLength(45).WithMessage("The last name cannot exceed 45 characters.");

			RuleFor(x => x.Person.Email)
				.NotEmpty().WithMessage("The email is required.")
				.EmailAddress().WithMessage("Invalid email format.")
				.MaximumLength(45).WithMessage("The email cannot exceed 45 characters.");

			RuleFor(x => x.Person.BirthDate)
				.NotEmpty().WithMessage("The birth date is required.")
				.LessThan(DateTime.Now).WithMessage("Birth date must be in the past.");

			
		}

	}
}
