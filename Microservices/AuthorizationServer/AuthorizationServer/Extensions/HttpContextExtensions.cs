using AuthorizationServer.Constants;
using AuthorizationServer.Errors;

namespace AuthorizationServer.Extensions;

public static class HttpContextExtensions
{
    public static async Task BadRequestAsync(this HttpContext context, ErrorCode code)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        await context.Response.WriteAsJsonAsync(new ResponseError(code));
    }
}