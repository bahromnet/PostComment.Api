using Application.Api.Common.Interfaces;
using Application.Api.Common.Notification;
using Domain.Api.Entities;
using MediatR;

namespace Application.Api.UseCases.Posts.Commands;

public class CreatePostCommand : IRequest<Guid>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid AuthorId { get; set; }
}

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMediator _mediator;
    public CreatePostCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IMediator mediator)
    {
        (_context, _currentUserService) = (context, currentUserService);
        _mediator = mediator;
    }

    public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = new Post
        {
            Title = request.Title,
            Content = request.Content,
            AuthorId = request.AuthorId,
            Created = DateTime.UtcNow,
            CreatedBy = _currentUserService.UserName
        };

        var entityId = await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync(cancellationToken);
        var name = _context.Users.FirstOrDefault(x => x.Id == post.AuthorId);
        await _mediator.Publish(new PostCreatedNotification()
        {
            PostContent = post.Content,
            Usernae = name.Username

        }, cancellationToken);

        return post.Id;
    }
}
