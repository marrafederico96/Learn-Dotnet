namespace FriendStuff.Dto;

public class GroupMemberDto
{
    public required string GroupName { get; init; }
    public required string NormalizeGroupName { get; init; }
    public required int NumberMember { get; init; }
    public required List<string> MemberUsername { get; init; }
}
