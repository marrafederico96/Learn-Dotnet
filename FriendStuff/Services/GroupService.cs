
using FriendStuff.Dto;
using FriendStuff.Models;
using FriendStuff.Repository;

namespace FriendStuff.Services;

public class GroupService(IGroupRepositoy groupRepositoy, IUserRepository userRepository) : IGroupService
{
    private IGroupRepositoy _groupRepository = groupRepositoy;
    private IUserRepository _userRepository = userRepository;
    public async Task CreateGroup(string GroupName, string AdminUsername)
    {
        var result = await this._groupRepository.FindGroup(GroupName, AdminUsername);
        var admin = await this._userRepository.FindUserByUsername(AdminUsername);
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
            GroupName = GroupName,
            Admin = admin,
        };

        await this._groupRepository.CreateGroup(newGroup);
        var group = await this._groupRepository.FindGroup(newGroup.GroupName, newGroup.Admin.Username) ?? throw new ArgumentException("Error creation group");
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

}
