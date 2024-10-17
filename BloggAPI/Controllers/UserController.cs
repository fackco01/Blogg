using DataAccess.Extension;
using DataAccess.IService;
using DataAccess.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

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
    }
}