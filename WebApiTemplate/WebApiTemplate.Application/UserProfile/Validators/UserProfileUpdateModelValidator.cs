using FluentValidation;
using FluentValidation.Results;
using WebApiTemplate.Application.UserProfile.Models;
using WebApiTemplate.Domain.Errors;

namespace WebApiTemplate.Application.UserProfile.Validators
{
    public class UserProfileUpdateModelValidator : AbstractValidator<UserProfileUpdateModel>
    {
        protected override bool PreValidate(ValidationContext<UserProfileUpdateModel> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new ValidationFailure(nameof(UserProfileUpdateModel), "null"));
                return false;
            }
            return base.PreValidate(context, result);
        }

        public UserProfileUpdateModelValidator()
        {
            RuleFor(x => x.DateOfBirth).NotNull().WithMessage(nameof(ErrorCodeValidation.DateOfBirthNull));
        }
    }
}
