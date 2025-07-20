using FriendStuff.Features.Auth.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FriendStuff.Features.Auth
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class AuthController(IAuthService authService, ITokenService tokenService) : ControllerBase
    {

        [HttpDelete]
        public async Task<IActionResult> Delete(UsernameDto usernameDto)
        {
            try
            {
                await authService.DeleteUser(usernameDto);
                return Ok(new { message = "User delete" });
            }
            catch (ArgumentException e)
            {
                return Unauthorized(new { message = e.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Error." });

            }
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var token = await authService.LoginUser(loginDto);

                HttpContext.Response.Cookies.Append("refreshToken", token.RefreshToken.ToString(), new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Path = "/api/auth/refresh",
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays(15),
                });

                return Ok(new { accessToken = token.AccessToken });
            }
            catch (ArgumentException e)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = e.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Error." });

            }
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await authService.RegisterUser(registerDto);
                return Ok(new { message = "User register" });
            }
            catch (ArgumentException e)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = e.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Error." });

            }
        }

        [HttpPost]
        public async Task<IActionResult> Refresh([FromBody] UsernameDto usernameDto)
        {
            try
            {
                HttpContext.Response.Cookies.Append("refreshToken", string.Empty, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Path = "/api/auth/refresh",
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays(-1),
                });

                TokenDto token = await tokenService.GenerateToken(usernameDto.Username);
                HttpContext.Response.Cookies.Append("refreshToken", token.RefreshToken.ToString(), new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Path = "/api/auth/refresh",
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays(15),
                });
                return Ok(new { accessToken = token.AccessToken });

            }
            catch (ArgumentException e)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = e.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Error." });
            }

        }

    }
}