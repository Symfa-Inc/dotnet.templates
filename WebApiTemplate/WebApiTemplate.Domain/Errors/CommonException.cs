namespace WebApiTemplate.Domain.Errors
{
    public class CommonException : Exception
    {
        public List<ErrorCode> Errors { get; set; }

        public CommonException(ErrorCode error)
        {
            Errors = new List<ErrorCode>
            {
                error
            };
        }

        public CommonException(List<ErrorCode> errors)
        {
            Errors = new List<ErrorCode>(errors);
        }
    }
}
