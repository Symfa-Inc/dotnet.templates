using System.ComponentModel;

namespace AuthorizationServer.Constants;

public enum ErrorCode
{
    [Description("Code is not provided.")]
    NoCodeProvided = 1,

    [Description("User have to log in.")]
    UnknownUser,

    [Description("User is not found.")]
    UserNotFound,

    [Description("Code is not valid.")]
    InvalidCode,

    [Description("Two-factor authentication is already enabled.")]
    TwoFactorAlreadyEnabled,

    [Description("Two-factor authentication is already disabled.")]
    TwoFactorAlreadyDisabled,

    [Description("Such provider is not exist.")]
    ProviderIsNotExist,

    [Description("Invalid credentials.")]
    InvalidCredentials,

    [Description("Refresh is not valid.")]
    InvalidRefreshToken,

    [Description("Two-factor authentication is required.")]
    TwoFactorAuthenticationRequired
}