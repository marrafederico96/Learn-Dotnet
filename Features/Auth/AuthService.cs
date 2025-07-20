using FriendStuff.Data;
using FriendStuff.Domain.Entities;
using FriendStuff.Features.Auth.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FriendStuff.Features.Auth;

public class AuthService(FriendStuffDbContext context, IPasswordHasher<User> passwordHasher, ITokenService tokenService) : IAuthService
{
    public async Task RegisterUser(RegisterDto registerDto)
    {
        var normalizeUsername = registerDto.Username.Trim().ToLowerInvariant();
        var normalizeEmail = registerDto.Email.Trim().ToLowerInvariant();
        var user = await context.Users.Where(u => u.NormalizeUsername.Equals(normalizeUsername) || u.Email.Equals(normalizeEmail)).FirstOrDefaultAsync();

        if (user != null)
        {
            throw new ArgumentException("User already exists");
        }

        User newUser = new()
        {
            Username = registerDto.Username,
            NormalizeUsername = normalizeUsername,
            Email = normalizeEmail,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            CreatedAt = DateTime.UtcNow
        };
        newUser.PasswordHash = passwordHasher.HashPassword(newUser, registerDto.Password);
        await context.AddAsync(newUser);
        await context.SaveChangesAsync();
    }

    public async Task<TokenDto> LoginUser(LoginDto loginDto)
    {
        string normalizeEmail = loginDto.Email.Trim().ToLowerInvariant();
        var user = await context.Users.Where(u => u.Email.Equals(normalizeEmail)).FirstOrDefaultAsync() ?? throw new ArgumentException("Wrong credentials");

        var passwordVerification = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);

        if (passwordVerification == 0)
        {
            throw new ArgumentException("Wrong credentials");
        }
        var jwt = await tokenService.GenerateToken(user.Username);
        return jwt;
    }

    public async Task DeleteUser(UsernameDto usernameDto)
    {
        var normalizeUsername = usernameDto.Username.Trim().ToLowerInvariant();
        var user = await context.Users.Where(u => u.NormalizeUsername.Equals(normalizeUsername)).FirstOrDefaultAsync() ?? throw new ArgumentException("User not found");
        context.Users.Remove(user);
        await context.SaveChangesAsync();
    }
}
