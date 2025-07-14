using System;
using FriendStuff.Dto;

namespace FriendStuff.Services
{
    /// <summary>
    /// Defines user-related operations.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="userData">An object containing the user's registration data.</param>
        /// <returns>An asynchronous task representing the registration operation.</returns>
        public Task RegisterUser(UserRegisterDto userData);

        /// <summary>
        /// Logs in a user and returns a JWT token if the credentials are valid.
        /// </summary>
        /// <param name="userData">An object containing the user's login credentials.</param>
        /// <returns>Set cookie for authenitcation.</returns>
        public Task LoginUser(UserLoginDto userData);


        /// <summary>
        /// Logout a user and invalidade Cookie.
        /// </summary>
        /// <param name="userData">An object containing the user's login credentials.</param>
        /// <returns>A JWT token as a string, if login is successful.</returns>
        public Task Logout();

        /// <summary>
        /// Deletes the currently authenticated user's account.
        /// </summary>
        /// <returns>An asynchronous task representing the user deletion operation.</returns>
        public Task DeleteUser();

        /// <summary>
        /// Get user logged info
        /// </summary>
        /// <param name="username">Find user by username</param>
        /// <returns>userInfoDto object</returns>
        public Task<UserInfoDto> GetUserInfo(string username);
    }
}
