using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cosmos.Application.Interfaces;
using Cosmos.Domain.Entities;
using Cosmos.Domain.Enums;
using Microsoft.IdentityModel.Tokens;

namespace Cosmos.Application.Services;

public class TokenService : ITokenService
{
    public string GenerateJWT(User user)
    {
        DotNetEnv.Env.Load("../../");

        var handler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET")!);

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor{
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddHours(8),
            Subject = GenerateClaims(user)
        };

        var token = handler.CreateToken(tokenDescriptor);

        return handler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaims(User user)
    {
        var ci = new ClaimsIdentity();

        ci.AddClaim(new Claim(ClaimTypes.Name, user.Email));
        ci.AddClaim(new Claim(ClaimTypes.Role, user.Role.ToString()));
        
        return ci;
    }
}
