namespace SalesAdminApi.AccessModule;

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using SalesAdminApi.CoreModule;

public static class AccessApiConfiguration
{
    public static IServiceCollection AddAccessServices(this IServiceCollection services, ConfigurationManager config)
    {
        services.AddAuthorization();
        services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            JwtValidationConfig(o, config["Jwt:Issuer"], config["Jwt:Audience"], config["Jwt:Key"]);
        });
        services.AddAuthorization();
        services.AddTransient<IAccessService, AccessService>();
        services.AddTransient<IAccessRepository, AccessRepository>(repo => new AccessRepository(config.GetConnectionString("Db") ?? ""));
        return services;
    }

    public static WebApplication UseAccess(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        RouteGroupBuilder accessApi = app.MapGroup("/access");
        accessApi.MapPost("/get-token", [AllowAnonymous] (UserDto user, IAccessService service) => service.GetToken(user, app.Configuration)).AddBaseEndpointConfiguration();
        return app;
    }

    static void JwtValidationConfig(JwtBearerOptions options, string? issuer, string? audience, string? key)
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key ?? "")),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true
        };
    }
}