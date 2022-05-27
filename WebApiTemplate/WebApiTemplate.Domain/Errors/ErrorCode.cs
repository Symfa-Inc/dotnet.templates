namespace WebApiTemplate.Domain.Errors
{
    public enum ErrorCode
    {
        ValidationError = 1,
        EntityAlreadyExists,
        EntityInvalidColumns,
        EntityNotFound,
        EmailServiceDisabled,
        EmailServiceInvalidConfig,
        UserProfileNotFound,
    }
}
