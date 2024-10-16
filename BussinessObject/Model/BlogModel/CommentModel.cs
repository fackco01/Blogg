using BussinessObject.Model.AuthModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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