using Microsoft.EntityFrameworkCore;

namespace ContectUs.Model
{
    public class ContectContext:DbContext
    {
        public DbSet<Contect> contects { get; set; }
        public ContectContext(DbContextOptions<ContectContext> options) : base(options)
        {

        }
    }
}
