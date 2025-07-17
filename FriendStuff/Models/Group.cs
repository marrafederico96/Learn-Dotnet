using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FriendStuff.Models;

public class Group
{
    [Key]
    public long GroupId { get; init; }

    [Required][MaxLength(100)]
    public required string GroupName { get; init; }

    [Required][MaxLength(100)]
    public required string NormalizedGroupName { get; init; }

    [Required]
    public long AdminId { get; init; }

    [ForeignKey(nameof(AdminId))]
    public User? Admin { get; init; }

    public ICollection<GroupMember> GroupMembers { get; } = [];
}
