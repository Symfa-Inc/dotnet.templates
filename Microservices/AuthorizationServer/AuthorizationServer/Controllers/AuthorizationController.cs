using AuthorizationServer.Interfaces.Handlers.Actions;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationServer.Controllers;

[ApiController]
[Route("connect")]
public class AuthorizationController : ControllerBase
{
    private readonly ITokenIssueHandler _tokenIssueHandler;
    private readonly IExternalProviderHandler _externalProviderHandler;

    public AuthorizationController(ITokenIssueHandler tokenIssueHandler, IExternalProviderHandler externalProviderHandler)
    {
        _tokenIssueHandler = tokenIssueHandler;
        _externalProviderHandler = externalProviderHandler;
    }

    [HttpPost("token")]
    public async Task Exchange()
    {
        await _tokenIssueHandler.IssueAsync(HttpContext);
    }

    [HttpGet("authorize")]
    [HttpPost("authorize")]
    public async Task Authorize(string provider)
    {
        await _externalProviderHandler.SignInAsync(
            HttpContext,
            provider,
            Url.Action("ExternalCallback", new { originalQuery = HttpContext.Request.QueryString }));
    }

    // External provider redirects to this endpoint after the authorization process
    [HttpGet("authorize/callback")]
    [HttpPost("authorize/callback")]
    public IActionResult ExternalCallback(string originalQuery)
    {
        // Redirect back to the authorize endpoint
        return LocalRedirect($"{Url.Action(nameof(Authorize))}{originalQuery}");
    }
}