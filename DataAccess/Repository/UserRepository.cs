using BussinessObject.ContextData;
using BussinessObject.Model.AuthModel;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BlogContext _blogContext;
        public UserRepository(BlogContext blogContext) 
        {
            _blogContext = blogContext;
        }
        public async Task<UserModel> Authenticate(LoginModel login)
        {
            var user = await _blogContext.users.FirstOrDefaultAsync(u => u.username == login.username && u.password == login.password);
            if (user == null)
            {
                return null;
            }

            return user;
        }

        public async Task<UserModel> VerifyToken(string token)
        {
            var userToken = await _blogContext.users.FirstOrDefaultAsync(u => u.verificationToken == token);
            if (userToken == null) 
            {
                return null;
            }

            userToken.verifiedAt = DateTime.UtcNow;
            await _blogContext.SaveChangesAsync();

            return userToken;
        }

        public bool CheckUser(string username, string password)
        {
            return _blogContext.users.Any(u => u.username == username && u.password == password);
        }

        public List<UserModel> GetListUsers()
        {
            return _blogContext.users.ToList();
        }

        public UserModel GetUserById(Guid id)
        {
            return _blogContext.users.FirstOrDefault(u => u.userId == id);
        }

        public UserModel GetUserByName(string username)
        {
            return _blogContext.users.FirstOrDefault(u => u.username == username);
        }

        public List<UserModel> GetUsersEmail(string email)
        {
            return _blogContext.users.Where(u => u.email == email).ToList();
        }

        public void UserDelete(Guid id)
        {
            var user = _blogContext.users.Find(id);
            if (user != null)
            {
                _blogContext.users.Remove(user);
                _blogContext.SaveChanges();
            }
        }

        public void UserRegister(UserModel user)
        {
            _blogContext.users.Add(user);
            _blogContext.SaveChanges();
        }

        public void UserUpdate(UserModel user)
        {
            _blogContext.users.Add(user);
            _blogContext.Entry(user).State = EntityState.Modified;
            _blogContext.SaveChanges();
        }
    }
}
