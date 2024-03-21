using Application.Models;
using FluentValidation;

namespace Application.Validations
{
	public class UserValidator : AbstractValidator<UserCreateModel>
	{
		public UserValidator()
		{
            RuleFor(c => c.Username).NotEmpty().WithMessage("Username can not be empty");

            RuleFor(c => c.Email).NotEmpty().WithMessage("E-mail addres can not be empty");
            RuleFor(c => c.Email).EmailAddress().WithMessage("E mail addres not in correct format");

            RuleFor(c => c.Password)
                               .NotEmpty()
            .WithMessage("Password can not be empty")
            .Matches("[A-Z]").WithMessage("Password must include UPPERCASE letters")
            .Matches("[a-z]").WithMessage("Password must include lowercase letters")
            .Matches("[0-9]").WithMessage("Password must include digits")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must include special chars");
        }
	}
}

