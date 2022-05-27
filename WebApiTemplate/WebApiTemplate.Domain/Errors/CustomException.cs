namespace WebApiTemplate.Domain.Errors
{
    public class CustomException : Exception
    {
        public ErrorResponse ErrorResponse { get; set; }

        public CustomException(ErrorCode errorCode)
        {
            ErrorResponse = new ErrorResponse(errorCode);
        }

        public CustomException(ErrorCode errorCode, List<ErrorResponseItem> details)
        {
            ErrorResponse = new ErrorResponse(errorCode, details);
        }
    }
}
