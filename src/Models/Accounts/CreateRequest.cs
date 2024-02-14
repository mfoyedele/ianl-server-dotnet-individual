namespace WebApi.Models.Accounts;

using System.ComponentModel.DataAnnotations;
using WebApi.Entities;

public class CreateRequest
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    [EnumDataType(typeof(Role))]
    public string Role { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public string State { get; set; }
    [Required]
    public string Country { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }

    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}