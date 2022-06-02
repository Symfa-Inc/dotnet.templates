using AuthorizationServer.Constants;
using AuthorizationServer.Models.TwoFactorAuthentication;
using FluentValidation;

namespace AuthorizationServer.Validators;

public class GoogleAuthenticationInfoValidator : AbstractValidator<GoogleAuthenticationInfoModel>
{
    public GoogleAuthenticationInfoValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationErrorCode.CodeRequired))
            .WithMessage("Code is required.");
    }
}