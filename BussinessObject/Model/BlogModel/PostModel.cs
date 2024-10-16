using BussinessObject.Model.AuthModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BussinessObject.Model.BlogModel
{
    [Table("Post")]
    public class PostModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid postId { get; set; }

        [Required] public Guid userId { get; set; }
        [Required] public string title { get; set; }
        public string content { get; set; }
        public string imageFile { get; set; }
        public int likeCount { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime publishAt { get; set; }
        public bool isActive { get; set; }

        [JsonIgnore]
        public virtual UserModel? user { get; set; }

        [JsonIgnore]
        public ICollection<Post_Tag>? post_Tags { get; set; }

        [JsonIgnore]
        public ICollection<CommentModel> comments { get; set; }
    }
}