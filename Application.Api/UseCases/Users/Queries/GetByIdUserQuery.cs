using Application.Api.Common.Exceptions;
using Application.Api.Common.Interfaces;
using Application.Api.Common.Models;
using AutoMapper;
using Domain.Api.Entities;
using MediatR;

namespace Application.Api.UseCases.Users.Queries;

public class GetByIdUserQuery : IRequest<UserGetDto>
{
    public Guid Id { get; init; }
}

public class GetByIdQueryHandler : IRequestHandler<GetByIdUserQuery, UserGetDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;


    public GetByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
            => (_context, _mapper) = (context, mapper);

    public async Task<UserGetDto> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Users.FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity is null)
            throw new NotFoundException(nameof(User), request.Id);


        var result = _mapper.Map<UserGetDto>(entity);
        return result;
    }
}
