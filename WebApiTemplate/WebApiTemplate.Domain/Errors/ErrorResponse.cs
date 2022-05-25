namespace WebApiTemplate.Domain.Errors
{
    public class ErrorResponse
    {
        public List<ErrorResponseItem> Errors { get; set; }

        public ErrorResponse()
        {
            Errors = new List<ErrorResponseItem>();
        }
    }

    public static class ErrorResponseExtension
    {
        public static ErrorResponse ToErrorResponse(this CommonException commonException)
        {
            var errorResponse = new ErrorResponse();

            foreach (var error in commonException.Errors)
            {
                errorResponse.Errors.Add(new ErrorResponseItem
                {
                    Error = error.ToString("G")
                });
            }

            return errorResponse;
        }
    }
}
