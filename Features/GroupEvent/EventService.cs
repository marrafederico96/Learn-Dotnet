using System;
using FriendStuff.Data;
using FriendStuff.Domain.Entities;
using FriendStuff.Features.GroupEvent.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FriendStuff.Features.GroupEvent;

public class EventService(FriendStuffDbContext context) : IEventService
{
    public async Task CreateEvent(EventDto eventDto)
    {
        var groupName = eventDto.GroupName.TrimEnd().TrimStart().ToLowerInvariant();
        var user = await context.Users
        .Where(u => u.NormalizeUsername.Equals(eventDto.Username.Trim().ToLowerInvariant())).Include(u => u.UserGroups).ThenInclude(g => g.Group)
        .FirstOrDefaultAsync() ?? throw new ArgumentException("User not found");

        if (user.UserGroups != null)
        {
            var group = user.UserGroups.Where(g => g.Group.NormalizeGroupName.Equals(groupName)).FirstOrDefault() ?? throw new ArgumentException("Group not found");
            var result = await context.Events.Where(e => e.NormalizeEventName.Equals(eventDto.EventName.TrimEnd().TrimStart().ToLowerInvariant()) && e.GroupId == group.GroupId).FirstOrDefaultAsync();
            if (result != null)
            {
                throw new ArgumentException("Event name already exixsts");
            }

            if (eventDto.EndDate < eventDto.StartDate)
            {
                throw new ArgumentException("End Date invalid");
            }

            var location = await context.Locations
            .Where(l => l.NormalizeLocationName.Equals(eventDto.LocationName.TrimEnd().TrimStart().ToLowerInvariant()))
            .FirstOrDefaultAsync();


            if (location != null)
            {
                Event newEvent = new()
                {
                    EndDate = eventDto.EndDate,
                    StartDate = eventDto.StartDate,
                    NormalizeEventName = eventDto.EventName.TrimEnd().TrimStart().ToLowerInvariant(),
                    EventCategory = eventDto.EventCategory,
                    EventDescription = eventDto.EventDescription,
                    EventName = eventDto.EventName,
                    GroupId = group.GroupId,
                    LocationId = location.LocationId
                };
                await context.Events.AddAsync(newEvent);
                await context.SaveChangesAsync();
            }
            else
            {
                Location newLocation = new()
                {
                    City = eventDto.City,
                    NormalizeLocationName = eventDto.LocationName.TrimEnd().TrimStart().ToLowerInvariant(),
                    LocationName = eventDto.LocationName,
                    StreetName = eventDto.StreetName,
                    StreetNumber = eventDto.StreetNumber
                };
                await context.Locations.AddAsync(newLocation);
                await context.SaveChangesAsync();

                Event newEvent = new()
                {
                    EndDate = eventDto.EndDate,
                    NormalizeEventName = eventDto.EventName.TrimEnd().TrimStart().ToLowerInvariant(),
                    StartDate = eventDto.StartDate,
                    EventCategory = eventDto.EventCategory,
                    EventDescription = eventDto.EventDescription,
                    EventName = eventDto.EventName,
                    GroupId = group.GroupId,
                    LocationId = newLocation.LocationId
                };
                await context.Events.AddAsync(newEvent);
                await context.SaveChangesAsync();
            }
        }
    }
}
