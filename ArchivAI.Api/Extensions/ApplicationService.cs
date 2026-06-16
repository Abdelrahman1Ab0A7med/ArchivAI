using ArchivAI.Application.Interfaces;
using ArchivAI.Infrastructure.Data;
using ArchivAI.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace ArchivAI.Api.Extensions
{
    public static class ApplicationService
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ArchivAIDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IAuthService, AuthService>(); 
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<DocumentService>();
            services.AddScoped<IAIService, AIService>();
            services.AddScoped<AIService>();
            services.AddScoped<AuthService>(); // Register AuthService for direct injection
            services.AddScoped<IBackGroundService, BackGroundService>();
            services.AddScoped<BackGroundService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!));
            var jwtkey = configuration["JWTSettings:Key"];
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["JWTSettings:Issuer"],
                        ValidAudience = configuration["JWTSettings:Audience"],
                        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(s: jwtkey))
                    };
                });

            return services;
        }
    }
}
