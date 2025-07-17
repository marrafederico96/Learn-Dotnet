using System.ComponentModel;

namespace FriendStuff.Models;

public enum GroupMemberRole
{
    [Description("ADMIN")]
    Admin,

    [Description("MEMBER")]
    Member
}
