using FriendStuff.Data;
using FriendStuff.Models;

namespace FriendStuff.Repository;


public interface IGroupRepositoy
{

    /// <summary>
    /// Create a group of user
    /// </summary>
    /// <param name="group">Data group</param>
    /// <returns></returns>
    public Task CreateGroup(Group group);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="group"></param>
    /// <returns></returns>
    public Task DeleteGroup(Group group);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="groupName"></param>
    /// <param name="adminUsername"></param>
    /// <returns></returns>
    public Task<Group?> FindGroup(string groupName, string adminUsername);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public Task AddMember(GroupMember user);

}

