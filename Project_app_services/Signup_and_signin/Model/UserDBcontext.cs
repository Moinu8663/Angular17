using Microsoft.EntityFrameworkCore;

namespace Signup_and_signin.Model
{
    public class UserDBcontext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public UserDBcontext(DbContextOptions<UserDBcontext> options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
