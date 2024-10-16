using BussinessObject.ContextData;
using BussinessObject.Model.AuthModel;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DataAccess.Repository
{
    public class UserRepository
    {
        //Check User Data

        #region CheckUser

        public static async Task<bool> CheckUser(string username, string password)
        {
            try
            {
                using (var context = new BlogContext())
                {
                    var user = await context.users
                        .FirstOrDefaultAsync(a => a.username == username && a.password == password);
                    return user != null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion CheckUser

        //Get Data User
        // Get Data User Profile (Async)

        #region GetProfileAsync

        public static async Task<UserModel> GetProfileAsync(ClaimsPrincipal claims)
        {
            try
            {
                using (var context = new BlogContext())
                {
                    // Trích xuất userId từ JWT token
                    var userIdClaim = claims.FindFirst(ClaimTypes.NameIdentifier);
                    if (userIdClaim == null)
                    {
                        throw new Exception("User ID not found in token.");
                    }

                    var userId = Guid.Parse(userIdClaim.Value);

                    var user = await context.users
                        .FirstOrDefaultAsync(u => u.userId == userId);
                    return user;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion GetProfileAsync

        //Get List Users Data

        #region GetListUsers

        public static async Task<List<UserModel>> GetListUsers()
        {
            try
            {
                var listUsers = new List<UserModel>();
                using (var context = new BlogContext())
                {
                    listUsers = await context.users.ToListAsync();
                    return listUsers;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion GetListUsers

        //Get User Data by Id

        #region GetUserId

        public static async Task<UserModel> GetUserId(Guid Id)
        {
            try
            {
                var user = new UserModel();
                using (var context = new BlogContext())
                {
                    user = await context.users
                        .FirstOrDefaultAsync(u => u.userId == Id);
                }
                return user;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion GetUserId

        //Register User

        #region Register

        public static async Task Register(UserModel user)
        {
            try
            {
                using (var context = new BlogContext())
                {
                    context.users.Add(user);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion Register

        //Update user

        #region UpdateUser

        public static async Task UpdateUser(Guid Id)
        {
            try
            {
                using (var context = new BlogContext())
                {
                    var user = await GetUserId(Id);
                    if (user == null)
                    {
                        throw new Exception("User not found");
                    }
                    context.Entry(user).State
                        = EntityState.Modified;
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion UpdateUser

        //Inactive User

        #region InactiveUser

        public static async Task InactiveUser(Guid Id)
        {
            try
            {
                using (var context = new BlogContext())
                {
                    var user = await GetUserId(Id);
                    if (user == null)
                    {
                        throw new Exception("User not found");
                    }

                    user.isActive = false; // Cập nhật trạng thái người dùng
                    context.Entry(user).State = EntityState.Modified;
                    await context.SaveChangesAsync(); // Cần lưu thay đổi
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion InactiveUser

        //Get user by email

        #region GetUserByEmail

        public static async Task<UserModel> GetUserByEmail(string email)
        {
            try
            {
                using (var context = new BlogContext())
                {
                    var user = await context.users.FirstOrDefaultAsync(u => u.email == email);
                    if (user == null)
                    {
                        throw new Exception("Email Doesn't Exist!!!!!!");
                    }
                    return user;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion GetUserByEmail

        //Get user by username

        #region GetUserByUsername

        public static async Task<UserModel> GetUserByUsername(string username)
        {
            try
            {
                using (var context = new BlogContext())
                {
                    var user = await context.users.FirstOrDefaultAsync(u => u.username == username);
                    if (user == null)
                    {
                        throw new Exception("User Doesn't Exist!!!!!!");
                    }
                    return user;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion GetUserByUsername
    }
}