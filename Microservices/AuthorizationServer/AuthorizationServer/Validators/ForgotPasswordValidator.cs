using AuthorizationServer.Constants;
using AuthorizationServer.Models.Account;
using FluentValidation;

namespace AuthorizationServer.Validators;

public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordModel>
{
    public ForgotPasswordValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationErrorCode.EmailRequired))
            .WithMessage("Email can not be empty.");
    }
}