using System;

namespace FriendStuff.Dto;

public class GroupMemberDto
{
    public required string GroupName { get; set; }
    public required int NumberMember { get; set; }
    public required List<string> MemberUsername { get; set; }
}
