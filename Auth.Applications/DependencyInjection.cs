
using Auth.Applications.Commons.Interfaces;
using Auth.Applications.Commons.Options;
using Auth.Applications.Db;
using Auth.Applications.Features.Users.Interfaces;
using Auth.Applications.Menus.Interfaces;
using Auth.Applications.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace Auth.Applications
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddAuthApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddDbContext<IAuthContext,AuthContext>(builderContextOpt =>
            {
                var connectionString = configuration.GetConnectionString("Auth");
                builderContextOpt.UseSqlServer(connectionString);
            });


            services.AddTransient<SqlDataConnection>(opt => {
                var connectionString = configuration.GetConnectionString("PortalDb");
                return new SqlDataConnection(connectionString);
            });

            services.AddScoped<IAuthManagement, AuthManagement>();
            services.AddScoped<IUserManagement, UserManagement>();
            services.AddScoped<IMenuManagement, MenuManagement>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            services.Configure<JWTOption>(configuration.GetSection("Jwt"));


            return services;
        }

        public static IServiceCollection AddJwtValidation(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });


            return services;
        }
    }
}
