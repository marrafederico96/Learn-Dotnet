using System;
using FriendStuff.Data;
using FriendStuff.Domain.Entities;
using FriendStuff.Features.EventExpense.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FriendStuff.Features.EventExpense.ExpenseRefund;

public class RefundService(FriendStuffDbContext context) : IRefundService
{
    public async Task AddRefund(RefundDto refundDto)
    {
        var payer = await context.Users
            .Where(u => u.NormalizeUsername.Equals(refundDto.PayerUsername.Trim().ToLowerInvariant()))
            .FirstOrDefaultAsync() ?? throw new ArgumentException("Payer not found");

        var debtor = await context.Users
            .Where(u => u.NormalizeUsername.Equals(refundDto.DebtorUsername.Trim().ToLowerInvariant()))
            .FirstOrDefaultAsync() ?? throw new ArgumentException("Debtor not found");

        var totalDebt = await context.ExpenseContributions
            .Include(ec => ec.Expense)
            .Where(ec => ec.ParticipantId == debtor.UserId && ec.Expense.PayerId == payer.UserId && ec.AmountOwed > 0)
            .SumAsync(c => c.AmountOwed);

        if (refundDto.Amount > totalDebt)
        {
            throw new InvalidOperationException("Refund amount exceeds current debt");
        }

        Refund newRefund = new()
        {
            Amount = refundDto.Amount,
            DebtorId = debtor.UserId,
            PayerId = payer.UserId,
            RefundDate = DateTime.UtcNow
        };
        await context.Refunds.AddAsync(newRefund);
        await context.SaveChangesAsync();
    }

}
