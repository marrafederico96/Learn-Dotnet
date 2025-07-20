
using FriendStuff.Data;
using FriendStuff.Domain.Entities;
using FriendStuff.Domain.Entities.Enums;
using FriendStuff.Features.Group.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FriendStuff.Features.Group.Member;

public class GroupMemberService(FriendStuffDbContext context) : IGroupMemberService
{
    public async Task AddMember(GroupMemberDto groupMemberDto)
    {
        var username = groupMemberDto.Username.Trim().ToLowerInvariant();
        var adminUsername = groupMemberDto.AdminUSername.Trim().ToLowerInvariant();
        var groupName = groupMemberDto.GroupName.TrimEnd().TrimStart().ToLowerInvariant();

        var user = await context.Users
        .Where(u => u.NormalizeUsername.Equals(username))
        .FirstOrDefaultAsync() ?? throw new ArgumentException("User not found");

        var admin = await context.Users
        .Where(u => u.NormalizeUsername.Equals(adminUsername))
        .FirstOrDefaultAsync() ?? throw new ArgumentException("User not found");

        var group = await context.UserGroups
        .Where(g => g.NormalizeGroupName.Equals(groupName) && g.AdminId == admin.UserId)
        .FirstOrDefaultAsync() ?? throw new ArgumentException("Group not found");


        if (user.UserGroups != null && user.UserGroups.Select(g => g.GroupId == group.GroupId).FirstOrDefault())
        {
            throw new ArgumentException("User already added");
        }

        GroupMember groupMember = new()
        {
            JoinDate = DateTime.UtcNow,
            Role = MemberRole.Member,
            GroupId = group.GroupId,
            UserId = user.UserId
        };
        await context.GroupMembers.AddAsync(groupMember);
        await context.SaveChangesAsync();
    }

    public async Task RemoveMember(GroupMemberDto groupMemberDto)
    {
        var username = groupMemberDto.Username.Trim().ToLowerInvariant();
        var adminUsername = groupMemberDto.AdminUSername.Trim().ToLowerInvariant();
        var groupName = groupMemberDto.GroupName.TrimEnd().TrimStart().ToLowerInvariant();

        var user = await context.Users
            .Where(u => u.NormalizeUsername.Equals(username))
            .FirstOrDefaultAsync() ?? throw new ArgumentException("User not found");

        var admin = await context.Users
            .Where(u => u.NormalizeUsername.Equals(adminUsername))
            .FirstOrDefaultAsync() ?? throw new ArgumentException("User not found");

        var group = await context.UserGroups
            .Where(g => g.NormalizeGroupName.Equals(groupName) && g.AdminId == admin.UserId)
            .FirstOrDefaultAsync() ?? throw new ArgumentException("Group not found");

        context.Remove(user);
        await context.SaveChangesAsync();
    }

}
