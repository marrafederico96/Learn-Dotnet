using System.ComponentModel.DataAnnotations;

namespace FriendStuff.Features.Auth.DTOs;

public record class RegisterDto
{
    [Required(ErrorMessage = "Username cannot be empty")]
    public required string Username { get; init; }

    [Required(ErrorMessage = "First Name cannot be empty")]
    public required string FirstName { get; init; }

    [Required(ErrorMessage = "Last Name cannot be empty")]
    public required string LastName { get; init; }

    [Required(ErrorMessage = "Email cannot be empty")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public required string Email { get; init; }

    [Required(ErrorMessage = "Password cannot be empty")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
    public required string Password { get; init; }

    [Required(ErrorMessage = "Confirm password cannot be empty")]
    [Compare("Password", ErrorMessage = "Password do not match")]
    public required string ConfirmPassword { get; init; }

}
