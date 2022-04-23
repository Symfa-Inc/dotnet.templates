using System.ComponentModel.DataAnnotations;

namespace AuthorizationServer.Models.Account;

public class RegistrationModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string Email { get; set; }
}