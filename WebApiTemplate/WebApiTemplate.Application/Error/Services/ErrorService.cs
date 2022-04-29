using WebApiTemplate.Application.Error.Interfaces;
using WebApiTemplate.Domain.Enums.Error;

namespace WebApiTemplate.Application.Error.Services
{
    public class ErrorService : IErrorService
    {
        public List<Models.Error> Errors { get; set; }

        public ErrorService()
        {
            Errors = new List<Models.Error>();
        }

        public void Add(Models.Error error)
        {
            Errors.Add(error);
        }

        public void Add(ErrorCode errorCode)
        {
            Errors.Add(new Models.Error
            {
                Code = (int)errorCode,
                Description = errorCode.ToString("G"),
            });
        }

        public void Add(ErrorCode errorCode, string value)
        {
            Errors.Add(new Models.Error
            {
                Code = (int)errorCode,
                Description = errorCode.ToString("G"),
                Value = value,
            });
        }

        public bool HasErrors
        {
            get
            {
                return Errors.Any();
            }
        }
    }
}
