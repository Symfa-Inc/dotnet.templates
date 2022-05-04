

namespace WebApiTemplate.Domain.Errors
{
    public class BaseException : Exception
    {
        public BaseException() { }
        public BaseException(string message)
            : base(message)
        {

        }
    }
}
