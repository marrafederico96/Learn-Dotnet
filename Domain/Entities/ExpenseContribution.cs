using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FriendStuff.Domain.Entities;

public class ExpenseContribution
{
    [Key]
    public Guid ExpenseContributionId { get; init; }

    [Required]
    public Guid ParticipantId { get; init; }

    [Required]
    public Guid ExpenseId { get; init; }

    [Required]
    public decimal AmountOwed { get; set; }

    [ForeignKey(name: "ParticipantId")]
    public User Participant { get; init; } = null!;

    [ForeignKey(name: "ExpenseId")]
    public Expense Expense { get; init; } = null!;
}
