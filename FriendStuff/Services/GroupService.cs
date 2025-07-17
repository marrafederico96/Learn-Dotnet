using FriendStuff.Dto;
using FriendStuff.Models;
using FriendStuff.Repository;

namespace FriendStuff.Services;


public class GroupService(IGroupRepository groupRepository, IUserRepository userRepository, NormalizeUrlString normalizeUrlString) : IGroupService
{
    private readonly IGroupRepository _groupRepository = groupRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly NormalizeUrlString _normalizeUrlString = normalizeUrlString;

    public async Task CreateGroup(string groupName, string adminUsername)
    {
        var result = await this._groupRepository.FindGroup(NormalizeUrlString.GenerateSlug(groupName));
        var admin = await this._userRepository.FindUserByUsername(adminUsername.Trim().ToLower());
        if (result != null)
        {
            throw new ArgumentException("Group already exists");
        }
        if (admin == null)
        {
            throw new ArgumentException("Admin not found");
        }

        Group newGroup = new()
        {
            GroupName = groupName.TrimEnd().TrimStart(),
            NormalizedGroupName = NormalizeUrlString.GenerateSlug(groupName),
            Admin = admin,
        };

        await this._groupRepository.CreateGroup(newGroup);

        GroupMember groupMember = new()
        {
            Group = newGroup,
            GroupMemberRole = GroupMemberRole.Admin,
            JoinData = DateTime.UtcNow,
            User = admin
        };

        await this._groupRepository.AddMember(groupMember);
    }

    public async Task AddMember(string username, string groupName)
    {
        var user = await this._userRepository.FindUserByUsername(username) ?? throw new ArgumentException("User not found");
        var group = await this._groupRepository.FindGroup(groupName) ?? throw new ArgumentException("Group not found");
        GroupMember groupMember = new()
        {
            Group = group,
            GroupMemberRole = GroupMemberRole.Member,
            JoinData = DateTime.UtcNow,
            User = user
        };
        await this._groupRepository.AddMember(groupMember);
    }

    public async Task<GroupMemberDto> FindGroup(string groupName)
    {
        var group = await this._groupRepository.FindGroup(groupName) ?? throw new ArgumentException("Group not found");
        GroupMemberDto groupMemberDto = new()
        {
            GroupName = group.GroupName,
            NormalizeGroupName = group.NormalizedGroupName,
            MemberUsername = [.. group.GroupMembers.Select(u => u.User.Username)],
            NumberMember = group.GroupMembers.Count
        };
        return groupMemberDto;
    }

    public async Task DeleteGroup(string groupName)
    {
        var group = await this._groupRepository.FindGroup(groupName) ?? throw new ArgumentException("Group not found");
        await this._groupRepository.DeleteGroup(group);

    }
}