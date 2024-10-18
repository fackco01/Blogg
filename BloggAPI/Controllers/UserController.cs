using DataAccess.Extension;
using DataAccess.IService;
using DataAccess.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BloggAPI.Controllers
{
    [EnableCors("AllowAllHeaders")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly BloggService _blogService;

        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, BloggService service, IConfiguration configuration)
        {
            _userService = userService;
            _blogService = service;
            _configuration = configuration;
        }

        //Get List User

        #region GetListUsers

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetListUsers()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var list = await _userService.GetListUsersAsync();

                if (list == null)
                {
                    return BadRequest("Không có người dùng");
                }

                // Convert to DTO
                var userListDtos = list.ToUserListDto();

                return Ok(userListDtos);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Lỗi hệ thống: {e.Message}");
            }
        }

        #endregion GetListUsers

        //Get Account Profile

        #region GetUserProfile

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserProfile()
        {
            try
            {
                // Lấy ID của user từ token đã được xác thực
                var userId = User.FindFirst("id")?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("User ID not found in token.");
                }

                // Chuyển đổi userId từ string sang Guid
                if (!Guid.TryParse(userId, out Guid userGuid))
                {
                    return BadRequest("Invalid user ID format.");
                }

                // Sử dụng userId để lấy profile của user
                var user = await _userService.GetUserByIdAsync(userGuid);

                if (user == null)
                {
                    return NotFound("User not found.");
                }

                var userDto = user.ToUserDto(); // Chuyển đổi thành DTO nếu cần

                return Ok(userDto);
            }
            catch (Exception e)
            {
                // Log the exception here
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        #endregion GetUserProfile
    }
}