using FriendStuff.Features.EventExpense.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FriendStuff.Features.EventExpense.ExpenseRefund
{
    [Route("api/group/event/expense/[controller]/[Action]")]
    [ApiController]
    public class RefundController(IRefundService refundService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] RefundDto refundDto)
        {
            try
            {
                await refundService.AddRefund(refundDto);
                return Ok(new { Message = "Refund add" });

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
