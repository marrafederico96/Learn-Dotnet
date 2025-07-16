using System;
using FriendStuff.Data;
using FriendStuff.Models;
using Microsoft.EntityFrameworkCore;

namespace FriendStuff.Repository;

public class UserRepository(FriendStuffDbContext context) : IUserRepository
{
    private readonly DbContext _context = context;

    public async Task RegisterUser(User user)
    {
        await this._context.AddAsync(user);
        await this._context.SaveChangesAsync();
    }

    public async Task DeleteUser(User user)
    {
        context.Remove(user);
        await this._context.SaveChangesAsync();
    }

    public async Task<User?> FindUserByUsernameOrEmail(string Username, string Email)
    {
        return await this._context.Set<User>().Include(u => u.MemberGroups).Include(u => u.GroupsAdmin).FirstOrDefaultAsync(u => u.Username.Equals(Username) || u.Email.Equals(Email));
    }

    public async Task<User?> FindUserByUsername(string Username)
    {
        return await this._context.Set<User>().Include(u => u.MemberGroups).Include(u => u.GroupsAdmin).FirstOrDefaultAsync(u => u.Username.Equals(Username));
    }
}
