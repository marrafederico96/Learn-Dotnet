using FriendStuff.Dto;
using FriendStuff.Models;
using FriendStuff.Repository;

namespace FriendStuff.Services;

public class GroupService(IGroupRepositoy groupRepositoy, IUserRepository userRepository, SlugString slugString) : IGroupService
{
    private readonly IGroupRepositoy _groupRepository = groupRepositoy;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly SlugString _slugString = slugString;
    public async Task CreateGroup(string GroupName, string AdminUsername)
    {
        var result = await this._groupRepository.FindGroup(GroupName.TrimEnd().TrimStart());
        var admin = await this._userRepository.FindUserByUsername(AdminUsername.Trim().ToLower());
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
            GroupName = GroupName.TrimEnd().TrimStart(),
            NormalizedGroupName = this._slugString.GenerateSlug(GroupName),
            Admin = admin,
        };

        await this._groupRepository.CreateGroup(newGroup);
        var group = await this._groupRepository.FindGroup(newGroup.GroupName.TrimEnd().TrimStart()) ?? throw new ArgumentException("Error creation group");
        GroupMember groupMemeber = new()
        {
            Group = group,
            GroupMemberRole = GroupMemberRole.ADMIN,
            JoinData = DateTime.UtcNow,
            User = admin,
        };

        await this._groupRepository.AddMember(groupMemeber);
    }

    public Task DeleteGroup(GroupDto groupData)
    {
        throw new NotImplementedException();
    }

    public Task AddMember(string username)
    {
        throw new NotImplementedException();
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
}
