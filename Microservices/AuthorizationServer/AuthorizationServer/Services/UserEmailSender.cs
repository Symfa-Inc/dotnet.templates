using System.Text.Encodings.Web;
using AuthorizationServer.Interfaces.Services;

namespace AuthorizationServer.Services;

public class UserEmailSender : IUserEmailSender
{
    private readonly IConfiguration _configuration;
    private readonly IEmailSender _emailSender;

    public UserEmailSender(IConfiguration configuration, IEmailSender emailSender)
    {
        _configuration = configuration;
        _emailSender = emailSender;
    }

    public async Task SendPasswordRecoveryLinkAsync(string userEmail, string recoveryToken)
        => await _emailSender.SendEmailAsync(userEmail, "Forgot password", GetRecoveryLinkHtml(userEmail, recoveryToken));

    private string GetRecoveryLinkHtml(string userEmail, string recoveryToken)
        => $"<a href='{_configuration["Urls:RecoveryLinkBasePath"]}"
            + $"?userEmail={UrlEncoder.Default.Encode(userEmail)}"
            + $"&token={UrlEncoder.Default.Encode(recoveryToken)}'>"
            + "Your password recovery link."
            + "</a>";
}