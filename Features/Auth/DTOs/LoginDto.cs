using System.ComponentModel.DataAnnotations;

namespace FriendStuff.Features.Auth.DTOs;

public record class LoginDto
{

    [Required(ErrorMessage = "Email cannot be empty")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public required string Email { get; init; }

    [Required(ErrorMessage = "Password cannot be empty")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
    public required string Password { get; init; }
}
