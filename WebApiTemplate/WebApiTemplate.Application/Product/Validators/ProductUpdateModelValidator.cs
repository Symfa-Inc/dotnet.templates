using WebApiTemplate.Application.Product.Models;
using FluentValidation;
using FluentValidation.Results;
using WebApiTemplate.Domain.Errors;

namespace WebApiTemplate.Application.Product.Validators
{
    public class ProductUpdateModelValidator : AbstractValidator<ProductUpdateModel>
    {
        protected override bool PreValidate(ValidationContext<ProductUpdateModel> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new ValidationFailure(nameof(ProductUpdateModel), "null"));
                return false;
            }
            return base.PreValidate(context, result);
        }

        public ProductUpdateModelValidator()
        {
            RuleFor(x => x.Name).NotNull()
                .WithMessage(ErrorCodeValidation.NameNull.ToString());
            RuleFor(x => x.Name).MinimumLength(5)
                .WithMessage(ErrorCodeValidation.NameMinimumLength.ToString());
        }
    }
}
