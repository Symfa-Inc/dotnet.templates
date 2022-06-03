namespace WebApiTemplate.Domain.Errors
{
    public enum ErrorCodeValidation
    {
        DateOfBirthNull = 1,
        NameNull,
        NameMinimumLength,
    }

    public static class ErrorCodeValidationExtension
    {
        public static string ToStringCached(this ErrorCodeValidation value)
        {
            return value switch
            {
                ErrorCodeValidation.DateOfBirthNull => nameof(ErrorCodeValidation.DateOfBirthNull),
                ErrorCodeValidation.NameNull => nameof(ErrorCodeValidation.NameNull),
                ErrorCodeValidation.NameMinimumLength => nameof(ErrorCodeValidation.NameMinimumLength),
                _ => value.ToString(),
            };
        }
    }
}
