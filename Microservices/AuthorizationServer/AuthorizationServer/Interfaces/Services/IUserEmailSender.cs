namespace AuthorizationServer.Interfaces.Services;

public interface IUserEmailSender
{
    Task SendPasswordRecoveryLinkAsync(string userEmail, string recoveryToken);
}