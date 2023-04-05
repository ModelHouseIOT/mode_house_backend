using System.ComponentModel.DataAnnotations;

namespace ModelHouse.Security.Domain.Services.Communication;

public class RegisterRequest
{
    [Required]
    [EmailAddress]
    public string EmailAddress { get; set; }
    [Required]
    public string Password { get; set; }
}