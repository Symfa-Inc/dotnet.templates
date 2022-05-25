
namespace WebApiTemplate.Domain.Errors
{
    public class CommonException : Exception
    {
        public ErrorCode Error { get; set; }
        public object Value { get; set; }

        public CommonException(ErrorCode error)
        {
            Error = error;
        }

        public CommonException(ErrorCode error, object value)
        {
            Error = error;
            Value = value;
        }
    }
}
