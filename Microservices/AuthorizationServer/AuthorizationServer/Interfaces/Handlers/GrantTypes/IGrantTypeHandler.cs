namespace AuthorizationServer.Interfaces.Handlers.GrantTypes;

public interface IGrantTypeHandler
{
    Task HandleAsync(HttpContext context);
}