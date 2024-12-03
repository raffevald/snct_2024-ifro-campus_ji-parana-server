using Application.Auth.Dtos;
using Domain.Entities;

namespace Application.Auth.Services
{
    public interface IJWTManagerService
    {
        JwtTokensDto? GenerateToken(string userName, List<UserClaims> userClaims);
    }
}
