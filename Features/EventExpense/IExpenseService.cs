using System;
using FriendStuff.Domain.Entities;
using FriendStuff.Features.EventExpense.DTOs;

namespace FriendStuff.Features.EventExpense;

public interface IExpenseService
{
    /// <summary>
    /// Add expense to Event
    /// </summary>
    /// <param name="expenseDto">Object rappresents Expense info</param>
    /// <returns></returns>
    public Task AddExpense(ExpenseDto expenseDto);
    public Task<decimal> CalcolateBalance(BalanceDto balanceDto);

}
