using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BussinessObject.Model.BlogModel
{
    [Table("Tag")]
    public class TagModel
    {
        [Key]
        public int tagId { get; set; }

        public string title { get; set; }
        public string description { get; set; }

        [JsonIgnore]
        public ICollection<Post_Tag> post_Tags { get; set; }
    }
}