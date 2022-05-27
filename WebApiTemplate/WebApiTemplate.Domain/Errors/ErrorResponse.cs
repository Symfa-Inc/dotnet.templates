namespace WebApiTemplate.Domain.Errors
{
    public class ErrorResponse
    {
        public string Error { get; set; }
        public List<ErrorResponseItem> Details { get; set; }

        public ErrorResponse(ErrorCode errorCode)
        {
            Error = errorCode.ToString("G");
        }

        public ErrorResponse(ErrorCode errorCode, List<ErrorResponseItem> details)
        {
            Error = errorCode.ToString("G");
            Details = new List<ErrorResponseItem>(details);
        }
    }
}
