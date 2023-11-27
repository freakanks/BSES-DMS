using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace BSES.DocumentManagementSystem.Encryption.Data
{
    public class DMSEncryptionDBContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DMSEncryptionKey>().HasData(new DMSEncryptionKey[]
            {
                new DMSEncryptionKey(){CompanyCode = "BRPL", EncryptionKey = "xyq2FaIYqbcwTuUlsiQ0UbJWAUCfh2JlmJVvwQs5Khg=", EncryptionIV = "CaTOyz86Ad0TGyuWZts27A=="},
                new DMSEncryptionKey(){CompanyCode = "BYPL", EncryptionKey = "QxZ5q8IuyjOn+Pyp3nucakA9jaieuKSvYZD8qLwYOqk=", EncryptionIV = "G27D2ihSFZmREv17iBL0zQ=="}
            });
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseOracle(ConfigurationManager.ConnectionStrings["DMS_DATABASE"].ConnectionString);
        }

        public DbSet<DMSEncryptionKey> DMSEncryptionKeys { get; set; }
    }
}
