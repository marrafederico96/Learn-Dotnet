using FriendStuff.Dto;

namespace FriendStuff.Services
{
    public interface IUserService
    {
        public Task RegisterUser(UserRegisterDto userData);

        public Task LoginUser(UserLoginDto userData);

        public Task Logout();

        public Task DeleteUser(string username);

        public Task<UserInfoDto> GetUserInfo(string username);

        public Task<List<GroupMemberDto>> GetGroups(string username);
    }
}
