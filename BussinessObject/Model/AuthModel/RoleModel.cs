using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
