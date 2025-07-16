using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FriendStuff.Models;

public class Group
{
    [Key]
    public long GroupId { get; set; }

    [Required]
    public string GroupName { get; set; } = string.Empty;

    [Required]
    public long AdminId { get; set; }

    [ForeignKey(nameof(AdminId))]
    public User Admin { get; set; } = null!;

    public ICollection<GroupMember> GroupMembers { get; } = [];
}
