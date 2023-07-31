using Microsoft.EntityFrameworkCore;
using ShrtLy.Domain;

namespace ShrtLy.Infrastructure
{
    public class ShrtLyContext : DbContext
    {
        public ShrtLyContext(DbContextOptions<ShrtLyContext> options)
            : base(options)
        {
        }

        public DbSet<LinkEntity> Links { get; set; }
    }
}
