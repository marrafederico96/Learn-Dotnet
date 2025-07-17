using FriendStuff.Data;
using FriendStuff.Models;
using Microsoft.EntityFrameworkCore;

namespace FriendStuff.Repository;

public class UserRepository(FriendStuffDbContext context) : IUserRepository
{
    private readonly FriendStuffDbContext _context = context;

    public async Task RegisterUser(User user)
    {
        await this._context.AddAsync(user);
        await this._context.SaveChangesAsync();
    }

    public async Task DeleteUser(User user)
    {
        this._context.Remove(user);
        await this._context.SaveChangesAsync();
    }

    public async Task<User?> FindUserByUsernameOrEmail(string username, string email)
    {
        return await this._context.Set<User>().Include(u => u.MemberGroups).Include(u => u.GroupsAdmin).FirstOrDefaultAsync(u => u.Username.Equals(username) || u.Email.Equals(email));
    }

    public async Task<User?> FindUserByUsername(string username)
    {
        return await this._context.Set<User>().Include(u => u.MemberGroups).Include(u => u.GroupsAdmin).FirstOrDefaultAsync(u => u.Username.Equals(username));
    }
}
