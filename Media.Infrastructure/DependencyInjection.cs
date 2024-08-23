using Media.Application.Interfaces.Authentication;
using Media.Application.Interfaces.Persistence;
using Media.Application.Interfaces.Services;
using Media.Infrastructure.Authentication;
using Media.Infrastructure.Configurations;
using Media.Infrastructure.Data;
using Media.Infrastructure.Persistence;
using Media.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Media.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddAuth(configuration);

            services.Configure<MongoDBSettings>(configuration.GetSection(nameof(MongoDBSettings)));
            services.AddSingleton<MongoDBContext>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IPostRepository, PostRepository>();

            services.AddScoped<ICommentRepository, CommentRepository>();

            services.AddScoped<IReplyRepository, ReplyRepository>();

            services.AddScoped<IDateTimeProvider, DateTimeProvider>();

            return services;
        }

        public static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(JwtSettings), jwtSettings);

            services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                    };
                });

            return services;
        }
    }
}
