using System;
using FriendStuff.Features.Auth.DTOs;

namespace FriendStuff.Features.Auth;

public interface IAuthService
{

    /// <summary>
    /// Registers a new user using the provided registration details.
    /// </summary>
    /// <param name="registerDto">An object containing the user's registration data.</param>
    /// <returns>A task that represents the asynchronous registration operation.</returns>
    Task RegisterUser(RegisterDto registerDto);

    /// <summary>
    /// Authenticates a user using the provided login credentials.
    /// </summary>
    /// <param name="loginDto">An object containing the user's login credentials.</param>
    /// <returns>A task that represents the asynchronous login operation and return a JWT token authentication.</returns>
    public Task<TokenDto> LoginUser(LoginDto loginDto);


    /// <summary>
    /// Delete user
    /// </summary>
    /// <param name="usernameDto">Object contains username of user to delete</param>
    /// <returns></returns>
    public Task DeleteUser(UsernameDto usernameDto);
}
