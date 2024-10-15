using BussinessObject.Model.AuthModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Model.BlogModel
{
    [Table("Comment")]
    public class CommentModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid commentId { get; set; }
        [Required] public string commentText { get; set; }
        [Required] public DateTime createAt { get; set; }

        public virtual PostModel? post { get; set; }
        public virtual UserModel? user { get; set; }
    }
}
