using BussinessObject.ContextData;
using BussinessObject.Model.AuthModel;
using DataAccess.IService;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DataAccess.Service
{
    public class UserService : IUserService
    {
        private readonly BlogContext _context;

        public UserService(BlogContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckUserAsync(string username, string password)
        {
            try
            {
                return await UserRepository.CheckUser(username, password);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<UserModel> GetProfileAsync(ClaimsPrincipal claims)
        {
            try
            {
                return await UserRepository.GetProfileAsync(claims);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<UserModel>> GetListUsersAsync()
        {
            try
            {
                return await UserRepository.GetListUsers();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<UserModel> GetUserByIdAsync(Guid Id)
        {
            try
            {
                return await UserRepository.GetUserId(Id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task RegisterAsync(UserModel user)
        {
            try
            {
                await UserRepository.Register(user);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task UpdateUser(Guid Id)
        {
            try
            {
                await UserRepository.UpdateUser(Id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task InactivateUser(Guid Id)
        {
            try
            {
                await UserRepository.InactiveUser(Id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<UserModel> GetUserByEmailAsync(string email)
        {
            try
            {
                return await UserRepository.GetUserByEmail(email);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<UserModel> GetUserByUsernameAsync(string username)
        {
            try
            {
                return await UserRepository.GetUserByUsername(username);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<string> VerifyToken(string token)
        {
            try
            {
                return await UserRepository.VerifyToken(token);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}