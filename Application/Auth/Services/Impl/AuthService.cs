using Application.Auth.Commands;
using Application.Auth.Querys;

namespace Application.Auth.Services.Impl
{
    public class AuthService : IAuthService
    {
        public async Task<Domain.Shared.ApiResponse<TokenQuery>> AuthenticateAsync(UserLoginCommand userLogin)
        {
            throw new NotImplementedException();
        }
    }
}
