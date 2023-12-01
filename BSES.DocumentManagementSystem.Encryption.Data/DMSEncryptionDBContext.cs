using Microsoft.EntityFrameworkCore;

namespace BSES.DocumentManagementSystem.Encryption.Data
{
    public class DMSEncryptionDBContext : DbContext
    {
        public DMSEncryptionDBContext(DbContextOptions<DMSEncryptionDBContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DMSEncryptionKey>().HasData(new DMSEncryptionKey[]
            {
                new DMSEncryptionKey(){CompanyCode = "BRPL", EncryptionKey = "xyq2FaIYqbcwTuUlsiQ0UbJWAUCfh2JlmJVvwQs5Khg=", EncryptionIV = "CaTOyz86Ad0TGyuWZts27A=="},
                new DMSEncryptionKey(){CompanyCode = "BYPL", EncryptionKey = "QxZ5q8IuyjOn+Pyp3nucakA9jaieuKSvYZD8qLwYOqk=", EncryptionIV = "G27D2ihSFZmREv17iBL0zQ=="}
            });
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //options.UseOracle(configuration.GetConnectionString("DMS_DATABASE"));
            //options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DMSApplication;Integrated Security=True;Connection Timeout=15;MultipleActiveResultSets=true");
        }

        public DbSet<DMSEncryptionKey> DMSEncryptionKeys { get; set; }
    }
}
