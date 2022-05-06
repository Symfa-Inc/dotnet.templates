using AuthorizationServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationServer.Controllers;

[ApiController]
[Route("connect")]
public class AuthorizationController : ControllerBase
{
    private readonly ITokenIssueService _tokenIssueService;

    public AuthorizationController(ITokenIssueService tokenIssueService)
    {
        _tokenIssueService = tokenIssueService;
    }

    [HttpPost("token")]
    public async Task Exchange()
    {
        await _tokenIssueService.IssueAsync(HttpContext);
    }
}