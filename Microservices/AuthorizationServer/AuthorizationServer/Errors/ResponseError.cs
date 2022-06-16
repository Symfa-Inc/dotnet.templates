using AuthorizationServer.Constants;
using AuthorizationServer.Extensions;

namespace AuthorizationServer.Errors;

public class ResponseError
{
    public ResponseError(string error)
    {
        Error = error;
        Description = string.Empty;
    }

    public ResponseError(string error, string description)
    {
        Error = error;
        Description = description;
    }

    public ResponseError(ErrorCode code)
    {
        Error = code.ToString();
        Description = code.GetDescription();
    }

    public string Error { get; }
    public string Description { get; }
}