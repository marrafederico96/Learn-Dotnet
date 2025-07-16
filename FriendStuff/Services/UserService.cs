using System.Security.Claims;
using FriendStuff.Dto;
using FriendStuff.Models;
using FriendStuff.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

namespace FriendStuff.Services;

public class UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IHttpContextAccessor httpContext) : IUserService
{

    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
    private readonly IHttpContextAccessor _httpContext = httpContext;

    public async Task RegisterUser(UserRegisterDto userData)
    {
        User? user = await this._userRepository.FindUserByUsernameOrEmail(userData.Username.ToLower().Trim(), userData.Email.Trim().ToLower());

        if (user != null)
        {
            throw new ArgumentException("User already exists");
        }

        if (!userData.Password.Equals(userData.ConfirmPassword))
        {
            throw new ArgumentException("Password not match");
        }

        User newUser = new()
        {
            Username = userData.Username.Trim().ToLower(),
            Email = userData.Email.Trim().ToLower(),
            FirstName = userData.FirstName.TrimEnd().TrimStart(),
            LastName = userData.LastName.TrimEnd().TrimStart(),
        };
        newUser.PasswordHash = this._passwordHasher.HashPassword(newUser, userData.Password.TrimEnd().TrimStart());
        await this._userRepository.RegisterUser(newUser);
    }

    public async Task LoginUser(UserLoginDto userData)
    {
        User? user = await this._userRepository.FindUserByUsername(userData.Username.Trim().ToLower()) ?? throw new ArgumentException("Wrong credentials");

        if (this._passwordHasher.VerifyHashedPassword(user, user.PasswordHash, userData.Password) == 0)
        {
            throw new ArgumentException("Wrong credentials");
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Email, user.Email ),
            new(ClaimTypes.Role, "User"),
        };

        var claimsIdenty = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        if (this._httpContext.HttpContext != null)
        {
            await this._httpContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdenty));
        }
    }

    public Task DeleteUser()
    {
        throw new NotImplementedException();
    }

    public async Task Logout()
    {
        if (this._httpContext.HttpContext != null)
        {
            await this._httpContext.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }

    public async Task<UserInfoDto> GetUserInfo(string Username)
    {
        var user = await this._userRepository.FindUserByUsername(Username) ?? throw new ArgumentException("User not found");
        UserInfoDto userInfo = new()
        {
            Username = user.Username,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Groups = [.. user.MemberGroups.Select(g => new GroupMemberDto
            {
                GroupName = g.Group.GroupName,
                NormalizeGroupName = g.Group.NormalizedGroupName,
                MemberUsername = [.. g.Group.GroupMembers.Select(u => u.User.Username)],
                NumberMember = g.Group.GroupMembers.Count()

            })]
        };
        return userInfo;
    }

    public async Task<List<GroupMemberDto>> GetGroups(string Username)
    {
        var user = await this._userRepository.FindUserByUsername(Username) ?? throw new ArgumentException("User not found");

        var groupMemberDto = user.MemberGroups.Select(g => new GroupMemberDto
        {
            GroupName = g.Group.GroupName,
            NumberMember = g.Group.GroupMembers.Count,
            NormalizeGroupName = g.Group.NormalizedGroupName,
            MemberUsername = [.. g.Group.GroupMembers.Select(u => u.User.Username)]
        }).ToList();

        return groupMemberDto;
    }
}


