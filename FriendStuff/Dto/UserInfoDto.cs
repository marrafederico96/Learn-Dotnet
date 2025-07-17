namespace FriendStuff.Dto;

public class UserInfoDto
{
    public required string Username { get; init; }
    public required string LastName { get; init; }
    public required string FirstName { get; init; }
    public required string Email { get; init; }
    public List<GroupMemberDto>? Groups { get; set; }
}
