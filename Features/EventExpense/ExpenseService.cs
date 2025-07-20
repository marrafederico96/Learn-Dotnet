using System;
using FriendStuff.Data;
using FriendStuff.Domain.Entities;
using FriendStuff.Features.EventExpense.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FriendStuff.Features.EventExpense;

public class ExpenseService(FriendStuffDbContext context) : IExpenseService
{
    public async Task AddExpense(ExpenseDto expenseDto)
    {
        var normalizeUsername = expenseDto.PayerUsername.Trim().ToLowerInvariant();
        var normalizeEventName = expenseDto.EventName.TrimEnd().TrimStart().ToLowerInvariant();

        var payer = await context.Users
        .Where(u => u.NormalizeUsername.Equals(normalizeUsername))
        .FirstOrDefaultAsync() ?? throw new ArgumentException("User not found");

        var eventFound = await context.Events
        .Where(e => e.NormalizeEventName.Equals(normalizeEventName))
        .FirstOrDefaultAsync() ?? throw new ArgumentException("Event not found");


        Expense newExpense = new()
        {
            Amount = expenseDto.Amount,
            ExpenseName = expenseDto.ExpenseName,
            Description = expenseDto.Description,
            PayerId = payer.UserId,
            EventId = eventFound.EventId,
        };

        await context.Expenses.AddAsync(newExpense);
        await context.SaveChangesAsync();

        var normalizedParticipants = expenseDto.ParticipantUsername.Select(u => u.Trim().ToLowerInvariant()).Distinct().ToList();
        var participants = normalizedParticipants.Count + 1;
        var expenseContributions = new List<ExpenseContribution>();
        foreach (var username in normalizedParticipants)
        {
            var user = await context.Users
                .Where(u => u.NormalizeUsername.Equals(username))
                .Include(ec => ec.ExpenseContributions).ThenInclude(ex => ex.Expense)
                .FirstOrDefaultAsync() ?? throw new ArgumentException("User not found");

            expenseContributions.Add(new ExpenseContribution
            {
                ExpenseId = newExpense.ExpenseId,
                AmountOwed = Math.Round(newExpense.Amount / participants, 2, MidpointRounding.AwayFromZero),
                ParticipantId = user.UserId
            });
        }

        await context.ExpenseContributions.AddRangeAsync(expenseContributions);
        await context.SaveChangesAsync();
    }

    public async Task<decimal> CalcolateBalance(BalanceDto balanceDto)
    {
        var payer = await context.Users
            .Where(u => u.NormalizeUsername.Equals(balanceDto.PayerUsername.ToLowerInvariant().Trim()))
            .Include(u => u.ExpenseContributions).ThenInclude(ec => ec.Expense).Include(ec => ec.RefundsPaid).Include(ec => ec.RefundsReceived)
            .FirstOrDefaultAsync() ?? throw new ArgumentException("Payer not found");

        var debtor = await context.Users
            .Where(u => u.NormalizeUsername == balanceDto.DebtorUsername.ToLowerInvariant().Trim())
            .Include(u => u.ExpenseContributions).ThenInclude(ec => ec.Expense)
            .FirstOrDefaultAsync() ?? throw new ArgumentException("Debtor not found");

        var debtorOwesPayer = debtor.ExpenseContributions.Where(ec => ec.Expense.PayerId == payer.UserId).Sum(ec => ec.AmountOwed);
        var payerOwesDebtor = payer.ExpenseContributions.Where(ec => ec.Expense.PayerId == debtor.UserId).Sum(ec => ec.AmountOwed);

        var refundToPayer = payer.RefundsPaid.Where(r => r.DebtorId == debtor.UserId).Sum(r => r.Amount);
        var refundToDebtor = payer.RefundsReceived.Where(r => r.DebtorId == payer.UserId).Sum(r => r.Amount);

        return debtorOwesPayer - refundToPayer - payerOwesDebtor + refundToDebtor;
    }
}
