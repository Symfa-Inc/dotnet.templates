using FluentValidation;
using FluentValidation.Results;
using WebApiTemplate.Application.UserProfile.Models;
using WebApiTemplate.Domain.Errors;

namespace WebApiTemplate.Application.UserProfile.Validators
{
    public class UserProfileCreateModelValidator : AbstractValidator<UserProfileCreateModel>
    {
        protected override bool PreValidate(ValidationContext<UserProfileCreateModel> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new ValidationFailure(nameof(UserProfileCreateModel), "null"));
                return false;
            }
            return base.PreValidate(context, result);
        }

        public UserProfileCreateModelValidator()
        {
            RuleFor(x => x.DateOfBirth).NotNull().WithMessage(ErrorCodeValidation.DateOfBirthNull);
        }
    }
}
