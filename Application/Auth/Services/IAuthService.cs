using Application.Auth.Commands;
using Application.Auth.Dtos;
using Domain.Shared;

namespace Application.Auth.Services
{
    public interface IAuthService
    {
        Task<ApiResponse<TokenDto>> AuthenticateAsync(UserLoginCommand userLogin);
    }
}
