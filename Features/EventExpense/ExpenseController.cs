using FriendStuff.Features.EventExpense.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FriendStuff.Features.EventExpense
{
    [Authorize]
    [Route("api/group/event/[controller]/[Action]")]
    [ApiController]
    public class ExpenseController(IExpenseService expenseService) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Add(ExpenseDto expenseDto)
        {
            try
            {
                await expenseService.AddExpense(expenseDto);
                return Ok(new { message = "Expense added" });

            }
            catch (ArgumentException e)
            {
                return Unauthorized(new { message = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });

            }
        }

        [HttpPost]
        public async Task<IActionResult> Balance(BalanceDto balanceDto)
        {
            try
            {
                var balance = await expenseService.CalcolateBalance(balanceDto);
                return Ok(new { balance = balance });

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
