using Application.Auth.Commands;
using Application.Auth.Dtos;
using Domain.Entities;
using Domain.Helpers;
using Domain.Shared;

namespace Application.Auth.Services.Impl
{
    public class AuthService(
        IJWTManagerService jWTManagerService) : IAuthService
    {
        private readonly IJWTManagerService _jWTManagerService = jWTManagerService;

        private const string error = "invalid_credentials";


        public async Task<ApiResponse<TokenDto>> AuthenticateAsync(UserLoginCommand userLogin)
        {
            if (userLogin == null)
                return ApiResponseHelper.Unauthorized<TokenDto>("", error);

            User responseUserRepository = new User()
            {
                UserName = "oficina.ifro.2024@gmail.com",
                UserPassword = "uma senha muito forte",
                ExternalId = "dfads4rerewfasdfads98hjklll"
            };

            List<UserClaims> userClaims = new List<UserClaims>();
            UserClaims userClaim = new UserClaims()
            {
                ClaimType = "Admin",
                ClaimValue = "FullAccess"
            };
            userClaims.Add(userClaim);

            if ( userLogin.Password == responseUserRepository.UserPassword 
                && userLogin.UserName == responseUserRepository.UserName)
            {
                var responseJWTManagerService =  _jWTManagerService.GenerateToken(responseUserRepository.UserName, userClaims);
                
                if ( responseJWTManagerService?.JwtAccessToken is null ) 
                    return ApiResponseHelper.InternalServerError<TokenDto>("Erro ao fazer login");

                TokenDto tokenDto = new TokenDto()
                {
                    AccessToken = responseJWTManagerService.JwtAccessToken
                };

                return ApiResponseHelper.Ok(tokenDto, "Authentication successful.");
            }

            return ApiResponseHelper.Unauthorized<TokenDto>("", "Usuario ou senha invalido");
        }
    }
}
