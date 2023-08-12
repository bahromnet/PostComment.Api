using Application.Api.Common.Interfaces;
using Domain.Api.Entities;
using MediatR;

namespace Application.Api.UseCases.Comments.Commands;

public class CreateCommentCommand : IRequest<Guid>
{
    public string Content { get; init; }

    public Guid PostId { get; init; }
}

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateCommentCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
           => (_context, _currentUserService) = (context, currentUserService);

    public async Task<Guid> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {

        var entity = new Comment
        {
            Id = Guid.NewGuid(),
            Content = request.Content,
            AuthorId = _currentUserService.UserId,
            PostId = request.PostId,
            Created = DateTime.UtcNow,
            CreatedBy = _currentUserService.UserName,

        };
        await _context.Comments.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
