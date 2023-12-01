using Microsoft.EntityFrameworkCore;

namespace BSES.DocumentManagementSystem.Data
{
    public class DMSDBContext : DbContext
    {
        public DMSDBContext(DbContextOptions<DMSDBContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //options.UseOracle(configuration.GetConnectionString("DMS_DATABASE"));
            //options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DMSApplication;Integrated Security=True;Connection Timeout=15;MultipleActiveResultSets=true");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<AccessLog> AccessLogs { get; set; }
    }
}
