using System.Text;
using Homely.Application.Authentication;
using Homely.Application.Interfaces;
using Homely.Domain.Abstractions;
using Homely.Domain.Repositories;
using Homely.Infrastructure.Authentication;
using Homely.Infrastructure.Data;
using Homely.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Homely.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwt = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>()
                  ?? throw new InvalidOperationException("Missing jwt configuration.");

        if (string.IsNullOrWhiteSpace(jwt.Issuer) ||
            string.IsNullOrWhiteSpace(jwt.Audience) ||
            jwt.AccessTokenMinutes <= 0 ||
            jwt.RefreshTokenDays <= 0)
        {
            throw new InvalidOperationException("Invalid jwt configuration.");
        }

        byte[] secret;
        try
        {
            secret = Convert.FromBase64String(jwt.SecretBase64);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException("Jwt:SecretBase64 is not a valid Base64.", ex);
        }

        if (secret.Length < 32)
            throw new InvalidOperationException("Jwt keys must be at least 32 bytes long.");

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.MapInboundClaims = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwt.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwt.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    RequireExpirationTime = true,
                    RequireSignedTokens = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secret),
                    ValidAlgorithms = [SecurityAlgorithms.HmacSha256],
                    NameClaimType = "sub",
                    RoleClaimType = "role"

                    // TODO: verificar essa implementacao
                    // IssuerSigningKey = new SymmetricSecurityKey(
                    //     Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)),
                };
            });


        services.AddHttpContextAccessor();

        services.AddSingleton(jwt);
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITokenProvider, TokenProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<ICurrentUser, CurrentUser>();

        return services;
    }
}