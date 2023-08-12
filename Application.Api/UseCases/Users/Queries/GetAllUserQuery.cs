using Application.Api.Common.Interfaces;
using Application.Api.Common.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Api.UseCases.Users.Queries;

public class GetAllUserQuery : IRequest<List<UserGetDto>>
{
}

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUserQuery, List<UserGetDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(IApplicationDbContext context, IMapper mapper)
     => (_context, _mapper) = (context, mapper);

    public async Task<List<UserGetDto>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var entities = _context.Users.Include(x => x.Roles);
        var result = _mapper.Map<List<UserGetDto>>(entities);
        return result;
    }
}
