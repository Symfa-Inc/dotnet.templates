namespace AuthorizationServer.Services.Interfaces;

public interface ITokenIssueService
{
    Task IssueAsync(HttpContext context);
}