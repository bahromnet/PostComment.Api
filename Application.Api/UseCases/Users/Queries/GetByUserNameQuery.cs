using Application.Api.Common.Exceptions;
using Application.Api.Common.Interfaces;
using Application.Api.Common.Models;
using AutoMapper;
using Domain.Api.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Api.UseCases.Users.Queries;

public class GetByUserNameQuery : IRequest<UserGetDto>
{
    public string UserName { get; init; }
}

public class GetByUserNameQueryHandler : IRequestHandler<GetByUserNameQuery, UserGetDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;


    public GetByUserNameQueryHandler(IApplicationDbContext context, IMapper mapper)
            => (_context, _mapper) = (context, mapper);

    public async Task<UserGetDto> Handle(GetByUserNameQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Users.SingleOrDefaultAsync(x => x.Username == request.UserName, cancellationToken);

        if (entity is null)
            throw new NotFoundException(nameof(User), request.UserName);


        var result = _mapper.Map<UserGetDto>(entity);
        return result;
    }
}
