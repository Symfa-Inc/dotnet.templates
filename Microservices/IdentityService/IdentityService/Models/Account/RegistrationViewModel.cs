using System.ComponentModel.DataAnnotations;

namespace IdentityService.Models.Account;

public class RegistrationViewModel
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}