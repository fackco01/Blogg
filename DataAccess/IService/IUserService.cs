using BussinessObject.Model.AuthModel;
using System.Security.Claims;

namespace DataAccess.IService
{
    public interface IUserService
    {
        // Check if a user exists with the given username and password
        Task<bool> CheckUserAsync(string username, string password);

        // Get user profile based on claims (e.g., from JWT token)
        Task<UserModel> GetProfileAsync(ClaimsPrincipal userClaims);

        // Get a list of all users
        Task<List<UserModel>> GetListUsersAsync();

        // Get user by ID
        Task<UserModel> GetUserByIdAsync(Guid id);

        // Register a new user
        Task RegisterAsync(UserModel user);

        // Update user information
        Task UpdateUser(Guid id);

        // Inactivate a user
        Task InactivateUser(Guid id);

        // Get user by email
        Task<UserModel> GetUserByEmailAsync(string email);

        // Get user by username
        Task<UserModel> GetUserByUsernameAsync(string username);

        //Verify token
        Task<string> VerifyToken(string token);
    }
}