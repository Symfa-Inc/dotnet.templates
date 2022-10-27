using AuthorizationServer.Constants;
using AuthorizationServer.Models.Account;
using FluentValidation;

namespace AuthorizationServer.Validators;

public class RegistrationValidator : AbstractValidator<RegistrationModel>
{
    public RegistrationValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationErrorCode.UserNameRequired))
            .WithMessage("Username can not be empty.");
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationErrorCode.EmailRequired))
            .WithMessage("Email can not be empty.");
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationErrorCode.PasswordRequired))
            .WithMessage("Password can not be empty.");
    }
}