using BSES.DocumentManagementSystem.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace BSES.DocumentManagementSystem.Data
{
    public static class RegisterDataServicesDB
    {
        public static IServiceCollection AddDataServicesDB(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<DMSDBContext>(options =>
                                                {
                                                    options.UseOracle(configuration.GetConnectionString("DMS_DATABASE"));
                                                })
                    .AddScoped<IDocumentEntityDA, DocumentDA>()
                    .AddScoped<IDocumentUserEntityDA, DocumentUserEntityDA>()
                    .AddScoped<IDocumentLogEntityDA, DocumentLogEntityDA>();
    }
}
