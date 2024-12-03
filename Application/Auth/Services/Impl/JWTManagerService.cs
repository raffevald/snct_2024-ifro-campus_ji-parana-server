using Application.Auth.Dtos;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Auth.Services.Impl
{
    public class JWTManagerService(
        IConfiguration configuration) : IJWTManagerService
    {
        private readonly IConfiguration _configuration = configuration;

        public JwtTokensDto? GenerateToken(string userName, List<UserClaims> userClaims)
        {
            return GenerateJWTTokens(userName, userClaims);
        }

        public JwtTokensDto? GenerateJWTTokens(string userName, List<UserClaims> userClaims)
        {
            try
            {
                JwtTokensDto jwtTokensDto = new JwtTokensDto();

                var date = DateTime.UtcNow;
                var expiresDateRefreshToken = DateTime.UtcNow;
                var claims = GetUserClaims(userName, userClaims);

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(_configuration["JwtConfiguration:SecretKey"]!);
                var expires = int.Parse(_configuration["JwtConfiguration:AccessTokenExpires"]!);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),

                    Expires = date.AddMilliseconds(expires),
                    NotBefore = date,
                    Issuer = _configuration["JwtConfiguration:Issuer"],
                    Audience = _configuration["JwtConfiguration:Audience"],
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);


                jwtTokensDto.JwtAccessToken = tokenHandler.WriteToken(token);

                return jwtTokensDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        private List<Claim> GetUserClaims(string userName, List<UserClaims> userClaimsView)
        {
            var claims = new List<Claim>();

            foreach (var userClaim in userClaimsView)
            {
                claims.Add(new Claim(ClaimTypes.Name, userName));
                claims.Add(new Claim(ClaimTypes.Role, userClaim.ClaimType));
                claims.Add(new Claim("CustomClaimType", userClaim.ClaimValue));
                claims.Add(new Claim(ClaimTypes.Email, userClaim.UserClaimExternalId));
            }

            return claims;
        }
    }
}
