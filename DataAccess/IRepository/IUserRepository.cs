using BussinessObject.Model.AuthModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IUserRepository
    {
        Task<UserModel> Authenticate(LoginModel login);
        bool CheckUser(string username, string password);
        Task<UserModel> VerifyToken (string token);
        List<UserModel> GetListUsers();
        List<UserModel> GetUsersEmail(string email);
        UserModel GetUserById(Guid id);
        UserModel GetUserByName(string username);
        void UserRegister (UserModel user);
        void UserUpdate (UserModel user);
        void UserDelete (Guid id);
    }
}
