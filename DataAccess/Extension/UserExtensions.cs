using BussinessObject.Model.AuthModel;
using DataAccess.DTO;

namespace DataAccess.Extension
{
    public static class UserExtensions
    {
        public static UserDto ToUserDto(this UserModel user)
        {
            return new UserDto
            {
                userId = user.userId,
                username = user.username,
                password = user.password,
                email = user.email,
                phone = user.phone,
                fullName = user.fullName,
                birthDate = user.birthDate,
                gender = user.gender,
                verifiedAt = user.verifiedAt,
                roleId = user.roleId,
                verificationToken = user.verificationToken,
                isActive = user.isActive,
            };
        }

        public static LoginModel ToUserLogin(this UserLoginDto UserDTO)
        {
            return new LoginModel
            {
                username = UserDTO.username,
                password = UserDTO.password
            };
        }

        public static UserModel ToUserRegister(this RegisterDto registerDto, byte[] passwordHash, byte[] passwordSalt)
        {
            return new UserModel
            {
                username = registerDto.username,
                password = registerDto.password,
                passwordHash = passwordHash,
                passwordSalt = passwordSalt,
                email = registerDto.email,
                phone = registerDto.phone,
                fullName = registerDto.fullName,
                roleId = registerDto.roleId,
                isActive = registerDto.isActive,
            };
        }

        public static ListUserDto ToUserListDto(this UserModel user)
        {
            return new ListUserDto
            {
                username = user.username,
                email = user.email,
                phone = user.phone,
                fullName = user.fullName,
                birthDate = user.birthDate,
                gender = user.gender,
                isActive = user.isActive,
                verifiedAt = user.verifiedAt,
                roleId = user.roleId
            };
        }

        public static List<ListUserDto> ToUserListDto(this List<UserModel> users)
        {
            return users.Select(user => user.ToUserListDto()).ToList();
        }
    }
}