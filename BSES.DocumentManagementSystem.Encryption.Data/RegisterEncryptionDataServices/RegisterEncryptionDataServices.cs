using BSES.DocumentManagementSystem.Encryption.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BSES.DocumentManagementSystem.Encryption.Data
{
    public static class RegisterEncryptionDataServices
    {
        public static IServiceCollection AddEncryptionDataServices(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<DMSEncryptionDBContext>(options =>
            {
                //options.UseOracle(configuration.GetConnectionString("DMS_DATABASE"));
                options.UseSqlServer(configuration.GetConnectionString("DMS_DATABASE"));
                
            }, ServiceLifetime.Singleton)
            .AddSingleton<ISharedEncryptionKeysDA, SharedEncryptionKeysDA>();
    }
}
