using System.Net;
using AuthorizationServer.Errors;

namespace AuthorizationServer.Middlewares;

public class ExceptionHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new ResponseError(nameof(HttpStatusCode.InternalServerError)));
        }
    }
}