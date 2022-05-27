using FluentValidation.Results;
using WebApiTemplate.Domain.Errors;

namespace WebApiTemplate.Application.Extensions
{
    public static class FluentValidationErrorExtension
    {
        public static ErrorResponse ToErrorResponse(this ValidationResult validationResult)
        {
            var errorResponseItems = new List<ErrorResponseItem>();

            foreach (var validationError in validationResult.Errors)
            {
                errorResponseItems.Add(new ErrorResponseItem(validationError.PropertyName, validationError.ErrorMessage));
            }

            return new ErrorResponse(ErrorCode.ValidationError, errorResponseItems);
        }
    }
}
