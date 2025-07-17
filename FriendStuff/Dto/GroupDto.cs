using System.ComponentModel.DataAnnotations;

namespace FriendStuff.Dto;

public class GroupDto
{
    [Required(ErrorMessage = "Group Name cannot be empty")]
    public string GroupName { get; set; } = string.Empty;

}
