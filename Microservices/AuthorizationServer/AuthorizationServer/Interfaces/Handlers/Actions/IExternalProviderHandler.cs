namespace AuthorizationServer.Interfaces.Handlers.Actions;

public interface IExternalProviderHandler
{
    Task SignInAsync(HttpContext context, string provider, string redirectUrl);
}