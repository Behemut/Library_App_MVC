using Domain;
using Library_API.Models.Auth;
using Library_API.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using System.Text;

namespace Library_API.Extensions
{
    public static class IdentityServerExtension
    {

        public static IServiceCollection AddIdentityServices(this IServiceCollection services,
        IConfiguration config)
        {

            services.AddIdentity<UsersPerson, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            })
             .AddJwtBearer(options =>
             {
                 var key = Encoding.Default.GetBytes(Tokenizator.SecretKey);

                 options.SaveToken = true;
                 options.RequireHttpsMetadata = false;
                 options.TokenValidationParameters = new TokenValidationParameters()
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ClockSkew = TimeSpan.Zero,
                     ValidAudience = Tokenizator.AudienceToken,
                     ValidIssuer = Tokenizator.IssuerToken,
                     IssuerSigningKey = new SymmetricSecurityKey(key)
                 };
             });


            return services;
        }
    }
}
