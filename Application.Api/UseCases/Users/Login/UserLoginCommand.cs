using Application.Api.Common.Models;
using MediatR;

namespace Application.Api.UseCases.Users.Login;

public class UserLoginCommand : IRequest<TokenResponse>
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, TokenResponse>
{
    public Task<TokenResponse> Handle(UserLoginCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
