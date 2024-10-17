using BussinessObject.Model.BlogModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BussinessObject.Model.AuthModel
{
    [Table("User")]
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid userId { get; set; }

        [Required] public string username { get; set; }
        [Required] public string fullName { get; set; }
        [Required] public string phone { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [JsonIgnore]
        [NotMapped]
        [StringLength(20)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required] public bool gender { get; set; }
        [Required] public DateTime birthDate { get; set; }
        [Required] public bool isActive { get; set; }
        [Required] public int roleId { get; set; }

        // Additional properties
        public byte[]? passwordHash { get; set; } = new byte[32];

        public byte[]? passwordSalt { get; set; } = new byte[32];
        public string? verificationToken { get; set; }
        public DateTime? verificationTokenExpires { get; set; }
        public DateTime verifiedAt { get; set; }
        public string? passwordResetToken { get; set; }
        public DateTime resetTokenExpires { get; set; }

        [JsonIgnore]
        public virtual RoleModel? role { get; set; }

        [JsonIgnore]
        public ICollection<PostModel>? posts { get; set; }

        [JsonIgnore]
        public ICollection<CommentModel> comments { get; set; }
    }
}