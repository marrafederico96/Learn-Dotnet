using System.ComponentModel.DataAnnotations;

namespace FriendStuff.Models;

public class User
{
    [Key]
    public long UserId { get; init; }

    [Required][MaxLength(100)]
    public required string Username { get; init; }

    [Required][MaxLength(100)]
    public required string FirstName { get; init; }

    [Required][MaxLength(100)]
    public required string LastName { get; init; }

    [Required][MaxLength(100)]
    public required string Email { get; init; }

    [Required] [MaxLength(255)] public string PasswordHash { get; set; } = string.Empty;

    public ICollection<Group>? GroupsAdmin { get; } = [];

    public ICollection<GroupMember> MemberGroups { get; } = [];

}
