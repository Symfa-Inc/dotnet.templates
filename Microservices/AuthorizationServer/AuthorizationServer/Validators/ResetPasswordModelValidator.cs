using AuthorizationServer.Constants;
using AuthorizationServer.Models.Account;
using FluentValidation;

namespace AuthorizationServer.Validators;

public class ResetPasswordModelValidator : AbstractValidator<ResetPasswordModel>
{
    public ResetPasswordModelValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationErrorCode.EmailRequired))
            .WithMessage("Email can not be empty.");
        RuleFor(x => x.Token)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationErrorCode.TokenRequired))
            .WithMessage("Token can not be empty.");
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationErrorCode.PasswordRequired))
            .WithMessage("Password can not be empty.");
    }
}