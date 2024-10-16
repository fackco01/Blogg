using BussinessObject.Model.AuthModel;
using BussinessObject.Model.BlogModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BussinessObject.ContextData
{
    public class BlogContext : DbContext
    {
        public BlogContext()
        { }

        public virtual DbSet<UserModel> users { get; set; }
        public virtual DbSet<RoleModel> roles { get; set; }
        public virtual DbSet<PostModel> posts { get; set; }
        public virtual DbSet<TagModel> tags { get; set; }
        public virtual DbSet<CommentModel> comments { get; set; }
        public virtual DbSet<Post_Tag> post_tag { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration configuration = builder.Build();
            object value = optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post_Tag>().HasKey(PostTag => new { PostTag.tagId, PostTag.postId });

            modelBuilder.Entity<RoleModel>().HasData(
                new RoleModel { roleId = 1, roleName = "Administration" },
                new RoleModel { roleId = 2, roleName = "User" }
            );
        }
    }
}