using FriendStuff.Features.Auth.DTOs;

namespace FriendStuff.Features.Auth;

/// <summary>
/// Defines the contract for a service that handles JWT token generation.
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Generates a JSON Web Token (JWT) for a given username.
    /// The generated token typically includes claims for username and role,
    /// and is signed using an RSA private key.
    /// </summary>
    /// <param name="username">The username for whom the token is to be generated.</param>
    /// <returns>TokenDto object contains accessToken and refreshToken.</returns>
    public Task<TokenDto> GenerateToken(string username);
}