using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using FriendStuff.Data;
using FriendStuff.Domain.Entities;
using FriendStuff.Features.Auth.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FriendStuff.Features.Auth;

public class TokenService(FriendStuffDbContext context) : ITokenService
{
    public async Task<TokenDto> GenerateToken(string username)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, username),
            new(ClaimTypes.Role, "USER"),
        };
        var claimsIdentity = new ClaimsIdentity(claims, "Bearer");

        string rsaPrivateKeyPath = "/etc/secrets/private.pem";
        string rsaPrivateKey = await File.ReadAllTextAsync(rsaPrivateKeyPath);
        RSA rsa = RSA.Create();
        rsa.ImportFromPem(rsaPrivateKey);
        var securityKey = new RsaSecurityKey(rsa);

        SecurityTokenDescriptor securityTokenDescriptor = new()
        {
            Subject = claimsIdentity,
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha256)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(securityTokenDescriptor);

        TokenDto tokenDto = new()
        {
            AccessToken = tokenHandler.WriteToken(token),
            RefreshToken = await GenerateRefreshToken(username)
        };

        return tokenDto;
    }

    private async Task<Guid> GenerateRefreshToken(string username)
    {
        var user = await context.Users.Where(u => u.Username.Equals(username)).Include(u => u.UserRefreshTokens).FirstOrDefaultAsync() ?? throw new ArgumentException("User not found");

        user.UserRefreshTokens?.ToList().ForEach(t => t.Active = false);

        RefreshToken refreshToken = new()
        {
            CreatedAt = DateTime.UtcNow,
            ExpireAt = DateTime.UtcNow.AddDays(15),
            Active = true,
            TokenValue = Guid.NewGuid(),
            UserId = user.UserId
        };

        await context.RefreshTokens.AddAsync(refreshToken);
        await context.SaveChangesAsync();
        return refreshToken.TokenValue;
    }
}
