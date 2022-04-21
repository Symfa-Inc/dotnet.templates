namespace WebApiTemplate.Domain.Errors.Email
{
    public class EmailServiceException: Exception
    {
        public EmailServiceException() { }
        public EmailServiceException(string message)
            : base(message)
        {

        }
    }
}
