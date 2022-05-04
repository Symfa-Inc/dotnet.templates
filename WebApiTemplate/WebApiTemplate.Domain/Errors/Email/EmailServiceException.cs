namespace WebApiTemplate.Domain.Errors.Email
{
    public class EmailServiceException: BaseException
    {
        public EmailServiceException() { }
        public EmailServiceException(string message)
            : base(message)
        {

        }
    }
}
