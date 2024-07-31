using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SalesAdminApi.AccessModule;

public interface IAccessService
{
    IResult GetToken(UserDto user, IConfiguration config);
    Task<IResult> GetUserById(int id);
}

public class AccessService(IAccessRepository accessRepository) : IAccessService
{
    public IResult GetToken(UserDto user, IConfiguration config)
    {
        if (user.UserName == "canni" && user.Password == "uca")
        {
            var issuer = config["Jwt:Issuer"] ?? "";
            var audience = config["Jwt:Audience"] ?? "";
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(config["Jwt:Key"] ?? "");
            var token = jwtTokenHandler.CreateToken(GenerateTokenDescriptor(user, audience, issuer, key));

            var jwtToken = jwtTokenHandler.WriteToken(token);

            return Results.Ok(jwtToken);
        }
        else
        {
            return Results.Unauthorized();
        }
    }

    public async Task<IResult> GetUserById(int id)
    {
        UserDto user = await accessRepository.GetUserById(id);
        return user is null ? Results.NotFound(user) : Results.Ok(user);
    }

    private SecurityTokenDescriptor GenerateTokenDescriptor(UserDto user, string audience, string issuer, byte[] key)
    {
        return new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", "1"),
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, user.UserName),
                    // the JTI is used for our refresh token which we will be convering in the next video
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
            Expires = DateTime.UtcNow.AddHours(6),
            Audience = audience,
            Issuer = issuer,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };
    }
}