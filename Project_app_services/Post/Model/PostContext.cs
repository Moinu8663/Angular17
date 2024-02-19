using Microsoft.EntityFrameworkCore;

namespace Post.Model
{
    public class PostContext:DbContext
    {
        public DbSet<UserPost> userpost { get; set; }
        public PostContext(DbContextOptions<PostContext> options) : base(options)
        {

        }
    }
}
