using Application.Api.Common.Exceptions;
using Application.Api.Common.Interfaces;
using Domain.Api.Entities;
using MediatR;

namespace Application.Api.UseCases.Posts.Commands;

public class DeletePostCommand : IRequest<bool>
{
    public Guid PostId { get; set; }
}

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public DeletePostCommandHandler(IApplicationDbContext context)
           => _context = context;

    public async Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _context.Posts.FindAsync(request.PostId);
        if (post == null)
        {
            throw new NotFoundException(nameof(Post), request.PostId);
        }

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
