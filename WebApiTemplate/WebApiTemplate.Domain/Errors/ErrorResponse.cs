namespace WebApiTemplate.Domain.Errors
{
    public class ErrorResponse
    {
        public string Error { get; set; }

        public ErrorResponse(ErrorCode errorCode)
        {
            Error = errorCode.ToString();
        }

        public ErrorResponse(string error)
        {
            Error = error;
        }
    }
}
