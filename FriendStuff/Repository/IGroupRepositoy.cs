using FriendStuff.Models;

namespace FriendStuff.Repository;


public interface IGroupRepository
{

    public Task CreateGroup(Group group);

    public Task DeleteGroup(Group group);

    public Task<Group?> FindGroup(string groupName);

    public Task AddMember(GroupMember user);

}

