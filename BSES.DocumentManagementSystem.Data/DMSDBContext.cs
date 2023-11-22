using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace BSES.DocumentManagementSystem.Data
{
    public class DMSDBContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseOracle(ConfigurationManager.ConnectionStrings["DMS_DATABASE"].ConnectionString);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<AccessLog> AccessLogs { get; set; }
    }
}
