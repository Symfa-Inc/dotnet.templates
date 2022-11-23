namespace WebApiTemplate.Domain.Errors
{
    public class CustomException : Exception
    {
        public ErrorResponse ErrorResponse { get; set; }

        public CustomException(ErrorResponse errorResponse)
        {
            ErrorResponse = errorResponse;
        }

        public CustomException(ErrorCode errorCode)
        {
            ErrorResponse = new ErrorResponse(errorCode);
        }
    }
}
