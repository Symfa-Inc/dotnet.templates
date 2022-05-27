namespace WebApiTemplate.Domain.Errors
{
    public class CommonException : Exception
    {
        public ErrorResponse ErrorResponse { get; set; }

        public CommonException(ErrorCode errorCode)
        {
            ErrorResponse = new ErrorResponse(errorCode);
        }

        public CommonException(ErrorCode errorCode, List<ErrorResponseItem> details)
        {
            ErrorResponse = new ErrorResponse(errorCode, details);
        }
    }
}
