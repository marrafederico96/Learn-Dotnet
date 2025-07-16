using System.ComponentModel.DataAnnotations;

namespace FriendStuff.Models;

public class User
{
    [Key]
    public long UserId { get; set; }

    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    public ICollection<Group>? GroupsAdmin { get; } = [];

    public ICollection<GroupMember> MemberGroups { get; } = [];

}
