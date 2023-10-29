using Auth.Applications.Commons.Interfaces;
using Auth.Applications.Commons.Options;
using Auth.Core.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Applications.Services;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JWTOption _jwtConfig;
    public JwtTokenGenerator(IOptions<JWTOption> option)
    {
        _jwtConfig = option.Value;
    }

    public string GenerateToken(User user)
    {
        var issuer = _jwtConfig.Issuer;
        var audience = _jwtConfig.Audience;
        var key = Encoding.ASCII.GetBytes(_jwtConfig.Key);

        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(new[] {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim("EmployeeId", user.UserName),
                new Claim(JwtRegisteredClaimNames.Name, user.FullName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            }),
            Expires = DateTime.Now.AddDays(1),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}


