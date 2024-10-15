using BussinessObject.Model.AuthModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IService
{
    public interface IUserService
    {
        Task<UserModel> Authenticate(LoginModel login);
        List<UserModel> GetAllUsers();
        UserModel GetUserById(Guid id);
        void Register(UserModel user);
        void Update(UserModel user);
        void Delete(Guid id);
        Task<UserModel> VerifyToken(string token);
    }
}
