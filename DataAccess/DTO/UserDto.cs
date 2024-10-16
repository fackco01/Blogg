using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataAccess.DTO
{
    public record UserDto
    {
        public Guid userId { get; set; }
        public string username { get; set; } = string.Empty;
        public string fullName { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string password { get; set; }
        public bool gender { get; set; }
        public DateTime birthDate { get; set; }
        public bool isActive { get; set; }
        public int roleId { get; set; }
        public string verificationToken { get; set; } = string.Empty;
        public DateTime verifiedAt { get; set; } = DateTime.UtcNow;
    }

    public record RegisterDto
    {
        [Required]
        public string username { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")]
        [DataType(DataType.Password)]
        public string password { get; set; } = string.Empty;
        [Required, Compare("password")]
        public string confirmPassword { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string email { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;
        public string fullName { get; set; } = string.Empty;
        [JsonIgnore]
        public int roleId { get; set; }
        [JsonIgnore]
        public bool isActive { get; set; }
    }

    public record UserLoginDto
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public record UserUpdateDto
    {
        public Guid userId { get; set; }
        public string username { get; set; } = string.Empty;
        public string fullName { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string password { get; set; }
        public bool gender { get; set; }
        public DateTime birthDate { get; set; }
        public bool isActive { get; set; }
        public int roleId { get; set; }
    }

    public record ResetPasswordDto
    {
        [Required]
        public string token { get; set; } = string.Empty;
        [Required]
        public string password { get; set; } = string.Empty;
        [Required, Compare("password")]
        public string confirmPassword { get; set; } = string.Empty;
    }
}