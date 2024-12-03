using Application.Auth.Commands;
using Application.Auth.Querys;
using Domain.Shared;

namespace Application.Auth.Services
{
    public interface IAuthService
    {
        Task<ApiResponse<TokenQuery>> AuthenticateAsync(UserLoginCommand userLogin);
    }
}
