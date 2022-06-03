using WebApiTemplate.Application.Product.Models;
using FluentValidation;
using FluentValidation.Results;
using WebApiTemplate.Domain.Errors;

namespace WebApiTemplate.Application.Product.Validators
{
    public class ProductCreateModelValidator : AbstractValidator<ProductCreateModel>
    {
        protected override bool PreValidate(ValidationContext<ProductCreateModel> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new ValidationFailure(nameof(ProductCreateModel), "null"));
                return false;
            }
            return base.PreValidate(context, result);
        }

        public ProductCreateModelValidator()
        {
            RuleFor(x => x.Name).NotNull()
                .WithMessage(nameof(ErrorCodeValidation.NameNull));
            RuleFor(x => x.Name).MinimumLength(5)
                .WithMessage(nameof(ErrorCodeValidation.NameMinimumLength));
        }
    }
}
