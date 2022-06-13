namespace AuthorizationServer.Interfaces.Handlers.Actions;

public interface ITokenIssueHandler
{
    Task IssueAsync(HttpContext context);
}