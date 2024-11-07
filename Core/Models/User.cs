using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class User
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    public bool IsActive { get; set; }
}
