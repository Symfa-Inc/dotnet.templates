namespace WebApiTemplate.Domain.Errors
{
    public enum ErrorCode
    {
        EntityAlreadyExists = 1,
        EntityInvalidColumns,
        EntityNotFound,
        EmailServiceDisabled,
        EmailServiceInvalidConfig,
        UserProfileNotFound,
    }
}
