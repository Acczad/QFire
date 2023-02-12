using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace ICD.Framework.Web
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureAuthorization(this IServiceCollection services, string secretKey, string issuer, string scheme)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(secretKey));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = scheme;
                x.DefaultChallengeScheme = scheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    LifetimeValidator = (DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters) =>
                    {
                        return expires > DateTime.UtcNow;
                    }
                };
            });

            return services;
        }
    }
}


