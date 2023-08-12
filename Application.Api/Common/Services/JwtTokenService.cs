using Application.Api.Common.Exceptions;
using Application.Api.Common.Interfaces;
using Application.Api.Common.Models;
using Application.Api.UseCases.Users.Login;
using Application.Api.UseCases.Users.Queries;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Api.Common.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly IMediator _mediatr;
    private readonly IConfiguration _configuration;
    private readonly IHashPassword _hashPassword;

    public JwtTokenService(IMediator mediatr, IConfiguration configuration, IHashPassword hashPassword)
            => (_mediatr, _configuration, _hashPassword) = (mediatr, configuration, hashPassword);


    public async ValueTask<TokenResponse> CreateTokenAsync(UserLoginCommand userLogin)
    {
        var hasUser = await _mediatr.Send(new GetByUserNameQuery { UserName = userLogin.Username });
        if (hasUser is null)
        {
            throw new NotFoundException(nameof(UserLoginCommand), userLogin.Username);
        }
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, hasUser.Username)
        };
        foreach (var role in hasUser.Roles!)
        {
            foreach (var permission in role.Permissions)
            {
                if (permission.PermissionName is not null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, permission.PermissionName));  
                }
            }
        }

        int minute = 5;
        if (int.TryParse(_configuration.GetRequiredSection("JWT:ExpiresInMinutes").Value, out int _minute))
        {
            minute = _minute;
        }
        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration.GetRequiredSection("JWT:Issuer").Value,
                audience: _configuration.GetRequiredSection("JWT:Audience").Value,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(minute),
                signingCredentials: new SigningCredentials(
                                        new SymmetricSecurityKey(
                                            Encoding.UTF8.GetBytes(
                                                _configuration.GetRequiredSection("JWT:Key").Value!)
                                            ),SecurityAlgorithms.HmacSha256)

            );
        var result = new TokenResponse()
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            RefreshToken = await GenerateRefreshTokenAsync()
        };

        return result;
    }

    public ValueTask<string> GenerateRefreshTokenAsync()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return ValueTask.FromResult(Convert.ToBase64String(randomNumber));
        }
    }

    public ValueTask<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token)
    {
        throw new NotImplementedException();
    }
}
