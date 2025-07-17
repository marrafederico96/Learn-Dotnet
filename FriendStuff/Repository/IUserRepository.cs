using FriendStuff.Models;

namespace FriendStuff.Repository;

public interface IUserRepository
{
    public Task RegisterUser(User user);

    public Task DeleteUser(User user);

    public Task<User?> FindUserByUsernameOrEmail(string username, string email);

    public Task<User?> FindUserByUsername(string username);
}
