using BSES.DocumentManagementSystem.Data.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace BSES.DocumentManagementSystem.Data
{
    public static class RegisterDataServicesDB
    {
        public static IServiceCollection AddDataServicesDB(this IServiceCollection services) =>
            services.AddScoped<DMSDBContext, DMSDBContext>()
                    .AddScoped<IDocumentEntityDA, DocumentDA>()
                    .AddScoped<IDocumentUserEntityDA, DocumentUserEntityDA>()
                    .AddScoped<IDocumentLogEntityDA, DocumentLogEntityDA>();
    }
}
