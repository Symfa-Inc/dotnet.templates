using WebApiTemplate.Domain.Enums.Error;

namespace WebApiTemplate.Application.Error.Interfaces
{
    public interface IErrorService
    {
        void Add(Models.Error error);
        void Add(ErrorCode errorCode);
        void Add(ErrorCode errorCode, string value);
        bool HasErrors { get; }
    }
}
