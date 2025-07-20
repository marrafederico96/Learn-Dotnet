

using FriendStuff.Features.GroupEvent.DTOs;

namespace FriendStuff.Features.GroupEvent;

public interface IEventService
{

    /// <summary>
    /// Create a evento into a group
    /// </summary>
    /// <param name="eventDto">Object rappresent Event information</param>
    /// <returns></returns>
    public Task CreateEvent(EventDto eventDto);

}
