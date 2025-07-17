using FriendStuff.Dto;

namespace FriendStuff.Services;

public class LoginProcessingService : ILoginProcessingService
{
    public required UserLoginDto UserData { get; set; }

}
