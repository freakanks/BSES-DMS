using BSES.DocumentManagementSystem.Encryption.Data.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace BSES.DocumentManagementSystem.Encryption.Data
{
    public static class RegisterEncryptionDataServices
    {
        public static IServiceCollection AddEncryptionDataServices(this IServiceCollection services) =>
            services.AddSingleton<DMSEncryptionDBContext>()
                    .AddSingleton<ISharedEncryptionKeysDA, SharedEncryptionKeysDA>();
    }
}
