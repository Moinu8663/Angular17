using Microsoft.EntityFrameworkCore;

namespace UserAccount.Model
{
    public class AccountDBcontext: DbContext
    {
        public DbSet<AccountDetails> AccDetails { get; set; }
        public AccountDBcontext(DbContextOptions<AccountDBcontext> options) : base(options)
        {

        }
    }
}
