using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FriendStuff.Domain.Entities;

public class Refund
{
    [Key]
    public Guid RefundId { get; init; }

    [Required]
    public Guid PayerId { get; init; }

    [Required]
    public Guid DebtorId { get; init; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public DateTime RefundDate { get; init; }

    [ForeignKey(name: "PayerId")]
    public User Payer { get; set; } = null!;

    [ForeignKey(name: "DebtorId")]
    public User Debtor { get; set; } = null!;

}
