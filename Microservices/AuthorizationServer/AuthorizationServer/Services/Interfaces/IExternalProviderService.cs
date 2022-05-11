namespace AuthorizationServer.Services.Interfaces;

public interface IExternalProviderService
{
    Task SignInAsync(HttpContext context, string provider, string redirectUrl);
}