using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BussinessObject.Model.AuthModel
{
    [Table("Role")]
    public class RoleModel
    {
        [Key]
        public int roleId { get; set; }

        [Required] public string roleName { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserModel> users { get; set; }
    }
}