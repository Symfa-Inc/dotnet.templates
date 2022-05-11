using AuthorizationServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationServer.Controllers;

[ApiController]
[Route("connect")]
public class AuthorizationController : ControllerBase
{
    private readonly ITokenIssueService _tokenIssueService;
    private readonly IExternalProviderService _externalProviderService;

    public AuthorizationController(ITokenIssueService tokenIssueService, IExternalProviderService externalProviderService)
    {
        _tokenIssueService = tokenIssueService;
        _externalProviderService = externalProviderService;
    }

    [HttpPost("token")]
    public async Task Exchange()
    {
        await _tokenIssueService.IssueAsync(HttpContext);
    }

    [HttpGet("authorize")]
    [HttpPost("authorize")]
    public async Task Authorize(string provider)
    {
        await _externalProviderService.SignInAsync(
            HttpContext,
            provider,
            Url.Action("ExternalCallback", new { originalQuery = HttpContext.Request.QueryString }));
    }

    // External provider redirects to this endpoint after the authorization process
    [HttpGet("authorize/callback")]
    [HttpPost("authorize/callback")]
    public virtual IActionResult ExternalCallback(string originalQuery)
    {
        // Redirect back to the authorize endpoint
        return LocalRedirect($"{Url.Action(nameof(Authorize))}{originalQuery}");
    }
}