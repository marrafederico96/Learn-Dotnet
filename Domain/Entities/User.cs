using System;
using System.ComponentModel.DataAnnotations;

namespace FriendStuff.Domain.Entities;

public class User
{

    [Key]
    public Guid UserId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Username { get; set; } = string.Empty;


    [Required]
    [MaxLength(100)]
    public string NormalizeUsername { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    public DateTime CreatedAt { get; set; }

    public ICollection<RefreshToken> UserRefreshTokens { get; set; } = [];
    public ICollection<GroupMember> UserGroups { get; set; } = [];
    public ICollection<Expense> Expenses { get; set; } = [];
    public ICollection<ExpenseContribution> ExpenseContributions { get; set; } = [];
    public ICollection<Refund> RefundsPaid { get; set; } = [];
    public ICollection<Refund> RefundsReceived { get; set; } = [];

}
