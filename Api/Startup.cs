using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Infrastructure.DataBase.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Application.Auth.Services;
using Application.Auth.Services.Impl;
using Microsoft.OpenApi.Models;


namespace Api
{
    public static class Startup
    {
        public static IServiceCollection AddConfigsServices(this IServiceCollection services, IConfiguration configuration)
        {
            AddScopedConfiguration(services);
            AddOthersConfiguration(services, configuration);
            AddDataBaseConfiguration(services, configuration);
            AddAuthenticationConfiguration(services, configuration);

            return services;
        }

        public static IServiceCollection AddDataBaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            string env = configuration.GetSection("Application:Environment").Value!;
            string lichSystemDb = configuration.GetSection($"ConnectionStrings:LichSystemDb_{env}").Value!;

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            services.AddDbContext<SystemDbContext>(options => {
                options.UseNpgsql(lichSystemDb);
            });

            return services;
        }

        public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(options => {
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                options.SignIn.RequireConfirmedEmail = true;
            }).AddEntityFrameworkStores<SystemDbContext>().AddDefaultTokenProviders();

            services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o => {
                var key = Encoding.UTF8.GetBytes(configuration["JwtConfiguration:SecretKey"]!);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtConfiguration:Issuer"],
                    ValidAudience = configuration["JwtConfiguration:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }

        public static IServiceCollection AddScopedConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJWTManagerService, JWTManagerService>();

            return services;
        }

        public static IServiceCollection AddOthersConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Oficina ifro", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:5173", "http://localhost:3000")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });

            return services;
        }
    }
}
