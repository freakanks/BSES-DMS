using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace BSES.DocumentManagementSystem.Encryption.Data
{
    public class DMSEncryptionDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseOracle(ConfigurationManager.ConnectionStrings["DMS_DATABASE"].ConnectionString);
        }

        public DbSet<DMSEncryptionKey> DMSEncryptionKeys { get; set; }
    }
}
