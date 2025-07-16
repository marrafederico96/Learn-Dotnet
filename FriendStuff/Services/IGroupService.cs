using FriendStuff.Dto;

namespace FriendStuff.Services;

public interface IGroupService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="groupData"></param>
    /// <returns></returns>
    public Task CreateGroup(string GroupName, string AdminUsername);


    /// <summary>
    /// 
    /// </summary>
    /// <param name="groupData"></param>
    /// <returns></returns>
    public Task DeleteGroup(GroupDto groupData);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public Task AddMember(string username);

}
