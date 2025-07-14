using System;
using FriendStuff.Models;

namespace FriendStuff.Repository;

public interface IUserRepository
{
    /// <summary>
    /// Registers a new user in the system.
    /// </summary>
    /// <param name="user">The user object containing registration data.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task RegisterUser(User user);

    /// <summary>
    /// Authenticates a user and logs them into the system.
    /// </summary>
    /// <param name="user">The user object containing login credentials.</param>
    /// <returns>
    /// A JWT token if authentication is successful. Also sets an HttpOnly cookie for session management.
    /// </returns>
    //public Task<string> LoginUser(User user);

    /// <summary>
    /// Deletes a user from the system.
    /// </summary>
    /// <param name="user">The user object to be deleted.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task DeleteUser(User user);

    /// <summary>
    /// Retrieves a user entity based on the specified username.
    /// </summary>
    /// <param name="Username, Email">The unique username  or email identifying the user to retrieve.</param>
    /// <returns>
    /// A task representing the asynchronous operation. 
    /// The task result contains the <see cref="User"/> object if found; otherwise, <c>null</c>.
    /// </returns>
    public Task<User?> FindUserByUsernameOrEmail(string Username, string Email);


    /// <summary>
    /// Retrieves a user entity based on the specified username.
    /// </summary>
    /// <param name="Username">The unique username identifying the user to retrieve.</param>
    /// <returns>
    /// A task representing the asynchronous operation. 
    /// The task result contains the <see cref="User"/> object if found; otherwise, <c>null</c>.
    /// </returns>
    public Task<User?> FindUserByUsername(string username);
}
