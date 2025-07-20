using FriendStuff.Features.Group.DTOs;
using FriendStuff.Features.Group.Member;
using Microsoft.AspNetCore.Mvc;

namespace FriendStuff.Features.Group
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class GroupController(IGroupService groupService, IGroupMemberService groupMemberService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GroupDto groupCreateDto)
        {
            try
            {
                await groupService.CreateGroup(groupCreateDto);
                return Ok(new { message = "Group create" });
            }
            catch (ArgumentException e)
            {
                return Unauthorized(new { message = e.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Error." });

            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] GroupDto groupDto)
        {
            try
            {
                await groupService.DeleteGroup(groupDto);
                return Ok(new { message = "Group delete" });
            }
            catch (ArgumentException e)
            {
                return Unauthorized(new { message = e.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Error." });

            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] GroupMemberDto groupMemberDto)
        {
            try
            {
                await groupMemberService.AddMember(groupMemberDto);
                return Ok(new { message = "User added to group" });

            }
            catch (ArgumentException e)
            {
                return Unauthorized(new { message = e.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Error." });

            }
        }

        [HttpDelete]
        public async Task<IActionResult> Remove([FromBody] GroupMemberDto groupMemberDto)
        {
            try
            {
                await groupMemberService.RemoveMember(groupMemberDto);
                return Ok(new { message = "Member removed" });
            }
            catch (ArgumentException e)
            {
                return Unauthorized(new { message = e.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Error." });

            }
        }

    }
}
