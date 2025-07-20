using System;
using FriendStuff.Features.Group.DTOs;

namespace FriendStuff.Features.Group;

public interface IGroupService
{
    /// <summary>
    /// Create group of user
    /// </summary>
    /// <param name="groupDto">Object rappresent group information</param>
    /// <returns></returns>
    public Task CreateGroup(GroupDto groupDto);

    public Task DeleteGroup(GroupDto groupDto);

}
