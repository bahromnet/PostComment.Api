using Application.Api.Common.Models;
using Application.Api.UseCases.Users.Login;
using System.Security.Claims;

namespace Application.Api.Common.Interfaces;

public interface IJwtTokenService
{
    ValueTask<TokenResponse> CreateTokenAsync(UserLoginCommand userLogin);
    ValueTask<string> GenerateRefreshTokenAsync();
    ValueTask<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);

}
