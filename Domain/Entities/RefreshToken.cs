using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FriendStuff.Domain.Entities;

public class RefreshToken
{
    [Key]
    public Guid RefreshId { get; init; }

    [Required]
    public required DateTime CreatedAt { get; init; }

    [Required]
    public required DateTime ExpireAt { get; set; }

    [Required]
    public Guid TokenValue { get; init; }

    [Required]
    public bool Active { get; set; }

    [Required]
    public Guid UserId { get; init; }

    [ForeignKey("UserId")]
    public User User { get; init; } = null!;


}
