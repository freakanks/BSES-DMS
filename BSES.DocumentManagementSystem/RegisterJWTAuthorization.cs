using BSES.DocumentManagementSystem.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BSES.DocumentManagementSystem
{
    public static class RegisterJWTAuthorization
    {
        public static IServiceCollection AddJWT(this IServiceCollection services, IConfiguration configuration)
        {
           services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration.GetValue<string>(DMSConstants.JWT_ISSUER_CONFIG_KEY),
                    ValidAudience = configuration.GetValue<string>(DMSConstants.JWT_AUDIENCE_CONFIG_KEY),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetValue<string>(DMSConstants.JWT_SECRET_KEY_CONFIG_KEY)!)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });
            services.AddAuthorization();
            return services;
        }
    }
}
