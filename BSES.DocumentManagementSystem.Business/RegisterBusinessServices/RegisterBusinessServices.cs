using BSES.DocumentManagementSystem.Business.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace BSES.DocumentManagementSystem.Business
{
    public static class RegisterBusinessServices
    {
        /// <summary>
        /// Registers all the business services dependencies.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddBusinessServices(this IServiceCollection services) =>
            services
                    //.AddSingleton<IEncryptorDecryptorBA, EncryptorDecryptorBA>()
                    .AddScoped<IUserManagementBA, UserManagementBA>()
                    .AddScoped<IDocumentManagementBA, DocumentManagementBA>();
    }
}
