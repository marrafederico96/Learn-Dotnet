using FriendStuff.Data;
using FriendStuff.Models;
using Microsoft.EntityFrameworkCore;

namespace FriendStuff.Repository;

public class GroupRepository(FriendStuffDbContext context) : IGroupRepository
{
    private readonly FriendStuffDbContext _context = context;
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

    public async Task<Group?> FindGroup(string groupName)
    {
        return await this._context.Set<Group>().Include(g => g.GroupMembers).ThenInclude(m => m.User).FirstOrDefaultAsync(g => g.NormalizedGroupName.Equals(groupName));
    }

    public async Task AddMember(GroupMember user)
    {
        await this._context.AddAsync(user);
        await this._context.SaveChangesAsync();
    }

}
