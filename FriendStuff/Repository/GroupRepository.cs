using System;
using FriendStuff.Data;
using FriendStuff.Models;
using Microsoft.EntityFrameworkCore;

namespace FriendStuff.Repository;

public class GroupRepository(FriendStuffDbContext context) : IGroupRepositoy
{
    private FriendStuffDbContext _context = context;
    public async Task CreateGroup(Group group)
    {
        await this._context.AddAsync(group);
        await this._context.SaveChangesAsync();
    }

    public async Task DeleteGroup(Group group)
    {
        this._context.Remove(group);
        await this._context.SaveChangesAsync();

    }

    public async Task<Group?> FindGroupAdmin(string groupName, string adminUsername)
    {
        return await this._context.Set<Group>().Include(g => g.GroupMembers).FirstOrDefaultAsync(g => g.GroupName.Equals(groupName) && g.Admin.Username.Equals(adminUsername));
    }

    public async Task AddMember(GroupMember user)
    {
        await this._context.AddAsync(user);
        await this._context.SaveChangesAsync();
    }

    public Task<Group?> FindGroup(string groupName, string adminUsername)
    {
        throw new NotImplementedException();
    }
}
