
namespace FriendStuff.Dto;

public class UserInfoDto
{

    public required string Username { get; set; }
    public required string LastName { get; set; }
    public required string FirstName { get; set; }
    public required string Email { get; set; }
    public required List<GroupMemberDto> Groups { get; set; }

}
