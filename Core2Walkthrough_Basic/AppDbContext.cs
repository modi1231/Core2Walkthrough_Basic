using Core2Walkthrough_Basic.Data;
using Microsoft.EntityFrameworkCore;

namespace Core2Walkthrough_Basic
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        // The bridge between the db and data object to send data to.
        public DbSet<USERS> USER_DBSet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
