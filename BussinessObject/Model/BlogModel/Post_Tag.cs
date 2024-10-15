using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Model.BlogModel
{
    public class Post_Tag
    {
        public Guid postId { get; set; }
        public int tagId { get; set; }
        public virtual PostModel? post { get; set; }
        public virtual TagModel? tag { get; set; }
    }
}
