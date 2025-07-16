using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FriendStuff.Models;

public class GroupMember
{
    [Key]
    public long GroupMemberId { get; set; }

    [Required]
    public long GroupId { get; set; }

    [Required]
    public long UserId { get; set; }

    [Required]
    [EnumDataType(typeof(GroupMemberRole))]
    public GroupMemberRole GroupMemberRole { get; set; }

    [Required]
    public DateTime JoinData { get; set; }

    [ForeignKey(nameof(GroupId))]
    public Group Group { get; set; } = null!;

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

}
