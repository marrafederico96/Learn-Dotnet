using System.ComponentModel.DataAnnotations;

namespace FriendStuff.Features.Auth.DTOs;

public record class TokenDto
{
    [Required]
    public required string AccessToken { get; init; }

    [Required]
    public required Guid RefreshToken { get; init; }

}
