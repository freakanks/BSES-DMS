using BSES.DocumentManagementSystem.Data.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace BSES.DocumentManagementSystem.Data.FileSystem
{
    public static class RegisterDataServicesFileSystem
    {
        public static IServiceCollection AddDataServicesFileSystem(this IServiceCollection services) =>
            services.AddScoped<IDocumentDA, DocumentDA>()
                    .AddGlobalFontResolver();
    }
}
