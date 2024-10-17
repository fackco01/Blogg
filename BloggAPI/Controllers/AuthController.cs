using DataAccess.DTO;
using DataAccess.Extension;
using DataAccess.IService;
using DataAccess.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BloggAPI.Controllers
{
    [EnableCors("AllowAllHeaders")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly BloggService _service;

        private readonly IConfiguration _configuration;

        public AuthController(IUserService userService, BloggService service, IConfiguration configuration)
        {
            _userService = userService;
            _service = service;
            _configuration = configuration;
        }

        //Login
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //var isValid = await _userService.CheckUserAsync(loginDto.username, loginDto.password);
                //if (!isValid)
                //{
                //    return BadRequest("USER NOT FOUND OR INVALID CREDENTIALS");
                //}

                var user = await _userService.GetUserByUsernameAsync(loginDto.username);
                if (user == null || !user.isActive)
                {
                    return BadRequest("USER NOT FOUND OR INACTIVE");
                }

                // Verify password
                if (!VerifyPasswordHash(loginDto.password, user.passwordHash, user.passwordSalt))
                {
                    return BadRequest("Incorrect Password!!!!!!");
                }

                // Check if account is verified
                if (user.verifiedAt == null)
                {
                    return BadRequest("PLS, VERIFY YOUR ACCOUNT !!!!");
                }

                if (user.userId == null)
                {
                    return StatusCode(500, "User ID cannot be null.");
                }

                // Create token claims
                //var claims = new List<Claim>
                //{
                //    new Claim(ClaimTypes.NameIdentifier, user.userId.ToString()),
                //    new Claim(ClaimTypes.Name, user.username),
                //    new Claim(ClaimTypes.Role, "User") // You can change role as needed
                //};

                var claims = new List<Claim>
                {
                    new Claim("id", user.userId.ToString()), // Tùy chỉnh tên claim
                    new Claim("name", user.username), // Tùy chỉnh tên claim
                    new Claim("role", "User") // Tùy chỉnh tên claim
                };

                // Generate JWT token
                string token = _service.CreateToken(claims, _configuration);

                // Set cookies for token and userId
                SetCookie("access_token", token, true);
                SetCookie("uid", _service.EncryptString(user.userId.ToString(), _configuration), false);

                return Ok(new { message = $"Welcome back, {user.username}!", token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        //Register
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Kiểm tra xem username hoặc email đã tồn tại chưa
            var existingUser = await _userService.GetUserByEmailAsync(registerDto.email);
            if (existingUser != null)
            {
                return BadRequest("Email already in use.");
            }

            existingUser = await _userService.GetUserByUsernameAsync(registerDto.username);
            if (existingUser != null)
            {
                return BadRequest("Username already in use.");
            }

            // Tạo password hash và salt
            CreatedPasswordHash(registerDto.password,
                out byte[] passwordHash,
                out byte[] passwordSalt);

            // Tạo đối tượng userModel
            var userModel = registerDto.ToUserRegister(passwordHash, passwordSalt);
            userModel.verificationToken = CreateRandomToken();
            userModel.verificationTokenExpires = DateTime.UtcNow.AddMinutes(10);
            userModel.roleId = 2; // Mặc định role là User
            userModel.isActive = false;

            try
            {
                await _userService.RegisterAsync(userModel);
                return Ok("User registered successfully.");
            }
            catch (Exception ex)
            {
                // Trả về BadRequest với thông điệp từ exception
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        //Verify Token
        [HttpPost]
        public async Task<IActionResult> Verify(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token is required.");
            }

            var user = await _userService.VerifyToken(token);
            if (user == null)
            {
                return BadRequest("INCORRECT TOKEN");
            }
            return Ok();
        }

        //Logout
        [HttpPost]
        public IActionResult Logout()
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                System.Diagnostics.Debug.Write("Cookie: " + cookie);
                Response.Cookies.Delete(cookie, new CookieOptions()
                {
                    IsEssential = true,
                    SameSite = SameSiteMode.None,
                    Secure = true
                });
            }

            return Ok();
        }

        //Set Cookie
        private void SetCookie(string name, string value, bool httpOnly)
        {
            Response.Cookies.Append(name, value, new CookieOptions()
            {
                IsEssential = true,
                Expires = DateTime.Now.AddHours(3),
                Secure = true,
                HttpOnly = httpOnly,
                SameSite = SameSiteMode.None
            });
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        //Create Password Hash
        private void CreatedPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        //Create Random Token
        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
    }
}