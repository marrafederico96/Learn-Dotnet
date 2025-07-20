using System.ComponentModel.DataAnnotations;

namespace FriendStuff.Features.Group.DTOs;

public record class GroupDto
{
    [Required(ErrorMessage = "Group name cannot be empty")]
    public required string GroupName { get; init; }

    [Required(ErrorMessage = "Admin username cannot be empty")]
    public required string AdminUsername { get; init; }


}
