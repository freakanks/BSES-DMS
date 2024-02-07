using Microsoft.EntityFrameworkCore;

namespace BSES.DocumentManagementSystem.Data
{
    public class DMSDBContext : DbContext
    {
        public DMSDBContext(DbContextOptions<DMSDBContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("MOBAPP");
            modelBuilder.Entity<User>(user => user.ToTable("USERS"));
            modelBuilder.Entity<Document>(user => user.ToTable("DOCUMENTS"));
            modelBuilder.Entity<AccessLog>(user => user.ToTable("ACCESSLOGS"));
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //options.UseOracle(configuration.GetConnectionString("DMS_DATABASE"));
            //options.UseOracle("Data Source=10.125.64.73:1521/ebstest;User Id=mobapp;Password=mobapp;Connection Timeout=15;");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<AccessLog> AccessLogs { get; set; }
    }
}
