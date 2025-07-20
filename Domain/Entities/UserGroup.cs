using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FriendStuff.Domain.Entities;

public class UserGroup
{

    [Key]
    public Guid GroupId { get; init; }

    [Required]
    [MaxLength(100)]
    public required string GroupName { get; set; }

    [Required]
    [MaxLength(100)]
    public required string NormalizeGroupName { get; set; }

    [Required]
    public required DateTime CreatedAt { get; set; }

    [Required]
    public required Guid AdminId { get; set; }

    [ForeignKey(name: "AdminId")]
    public User Admin { get; set; } = null!;

    public ICollection<GroupMember>? GroupUsers { get; set; }
}
