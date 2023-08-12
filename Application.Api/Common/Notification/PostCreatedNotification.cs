using Application.Api.Common.Interfaces;
using Application.Api.Common.Services;
using MediatR;
using System.Diagnostics;

namespace Application.Api.Common.Notification;

public class PostCreatedNotification : INotification
{
    public string Usernae { get; set; }
    public string PostContent { get; set; }
}

public class PostCreatedNotificationHandler : INotificationHandler<PostCreatedNotification>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly TelegramServices _telegramServices;

    public PostCreatedNotificationHandler(IApplicationDbContext dbContext, TelegramServices telegramServices)
    {
        _dbContext = dbContext;
        _telegramServices = telegramServices;
    }

    public async Task Handle(PostCreatedNotification notification, CancellationToken cancellationToken)
    {
        Debug.Print($"User : {notification.Usernae},\nPost : {notification.PostContent}");
        Console.WriteLine($"User : {notification.Usernae},\nPost : {notification.PostContent}");
        await _telegramServices.NotifyPostCreated(post: notification.PostContent, userName: notification.Usernae);
    }
}
