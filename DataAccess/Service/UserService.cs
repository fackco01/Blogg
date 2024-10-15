using BussinessObject.Model.AuthModel;
using DataAccess.IRepository;
using DataAccess.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserModel> Authenticate(LoginModel login)
        {
            return await _userRepository.Authenticate(login);
        }

        public void Delete(Guid id)
        {
            _userRepository.UserDelete(id);
        }

        public List<UserModel> GetAllUsers()
        {
            return _userRepository.GetListUsers();
        }

        public UserModel GetUserById(Guid id)
        {
            return _userRepository.GetUserById(id);
        }

        public void Register(UserModel user)
        {
            var existingUser = _userRepository.GetUsersEmail(user.email);
            if (existingUser != null) 
            {
                throw new Exception("User Already Exist");
            }
            _userRepository.UserRegister(user);
        }

        public void Update(UserModel user)
        {
            _userRepository.UserUpdate(user);
        }

        public async Task<UserModel> VerifyToken(string token)
        {
            var checkToken = await _userRepository.VerifyToken(token);
            if (checkToken == null) 
            {
                return null;
            }
            return checkToken;
        }
    }
}
