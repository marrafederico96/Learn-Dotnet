using FriendStuff.Dto;

namespace FriendStuff.Services;

public interface IGroupService
{
    public Task CreateGroup(string groupName, string adminUsername);

    public Task DeleteGroup(string groupName);

    public Task AddMember(string username, string groupName);

    public Task<GroupMemberDto> FindGroup(string groupName);

}
