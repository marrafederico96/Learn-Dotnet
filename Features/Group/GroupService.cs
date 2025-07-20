using System;
using FriendStuff.Data;
using FriendStuff.Domain.Entities;
using FriendStuff.Domain.Entities.Enums;
using FriendStuff.Features.Group.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FriendStuff.Features.Group;

public class GroupService(FriendStuffDbContext context) : IGroupService
{
    public async Task CreateGroup(GroupDto groupDto)
    {
        var admin = await context.Users
        .Where(u => u.NormalizeUsername.Equals(groupDto.AdminUsername.Trim().ToLowerInvariant()))
        .FirstOrDefaultAsync() ?? throw new ArgumentException("Admin not found");

        UserGroup newGroup = new()
        {
            GroupName = groupDto.GroupName,
            NormalizeGroupName = groupDto.GroupName.TrimEnd().TrimStart().ToLowerInvariant(),
            CreatedAt = DateTime.UtcNow,
            AdminId = admin.UserId,
        };

        await context.UserGroups.AddAsync(newGroup);
        await context.SaveChangesAsync();

        var group = await context.UserGroups
        .Where(g => g.NormalizeGroupName.Equals(newGroup.NormalizeGroupName) && g.AdminId == admin.UserId)
        .FirstOrDefaultAsync() ?? throw new ArgumentException("Group not found");

        GroupMember groupMember = new()
        {
            JoinDate = DateTime.UtcNow,
            Role = MemberRole.Admin,
            GroupId = group.GroupId,
            UserId = admin.UserId,
        };
        await context.GroupMembers.AddAsync(groupMember);
        await context.SaveChangesAsync();
    }

    public async Task DeleteGroup(GroupDto groupDto)
    {
        var group = await context.UserGroups
        .Where(g => g.NormalizeGroupName
            .Equals(groupDto.GroupName.TrimEnd().TrimStart().ToLowerInvariant()) && g.Admin.NormalizeUsername.Equals(groupDto.AdminUsername.Trim().ToLowerInvariant()))
        .FirstOrDefaultAsync() ?? throw new ArgumentException("Group not found");

        context.UserGroups.Remove(group);
        await context.SaveChangesAsync();
    }
}
