namespace AuthorizationServer.Handlers.Interfaces;

public interface IGrantTypeHandler
{
    Task HandleAsync(HttpContext context);
}