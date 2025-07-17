using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FriendStuff.Models;

public class GroupMember
{
    [Key]
    public long GroupMemberId { get; init; }

    [Required]
    public long GroupId { get; init; }

    [Required]
    public long UserId { get; init; }

    [Required]
    [EnumDataType(typeof(GroupMemberRole))]
    public GroupMemberRole GroupMemberRole { get; init; }

    [Required]
    public DateTime JoinData { get; init; }

    [ForeignKey(nameof(GroupId))]
    public required Group Group { get; init; }

    [ForeignKey(nameof(UserId))]
    public required User User { get; init; }

}
