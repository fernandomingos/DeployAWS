using DeployAWS.Application.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DeployAWS.Domain.Services
{
    public static class ServiceJwtAuth
    {
        public static string GenerateToken(UserDto userDto, IConfiguration configuration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = configuration.GetValue<string>("Jwt:SecretKey");
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userDto.FirstName),
                    new Claim(ClaimTypes.Role, userDto.Profile)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
