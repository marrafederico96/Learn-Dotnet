using FriendStuff.Features.GroupEvent.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FriendStuff.Features.GroupEvent
{
    [Authorize]
    [Route("api/group/[controller]/[Action]")]
    [ApiController]
    public class EventController(IEventService eventService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EventDto eventDto)
        {
            try
            {
                await eventService.CreateEvent(eventDto);
                return Ok(new { message = "Event created" });
            }
            catch (ArgumentException e)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });

            }
        }
    }
}
