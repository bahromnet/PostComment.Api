using Application.Api.Common.Services;
using MediatR;
using System.Diagnostics;

namespace Application.Api.Common.Notification;

public class ExceptionNotification : INotification
{
    public string? ExceptionString { get; set; }
}

public class ExceptionNotificationHandle : INotificationHandler<ExceptionNotification>
{
    private readonly TelegramServices _telegramServices;

    public ExceptionNotificationHandle(TelegramServices telegramServices)
    {
        _telegramServices = telegramServices;
    }

    public async Task Handle(ExceptionNotification notification, CancellationToken cancellationToken)
    {
        Debug.Print($"Exception : {notification.ExceptionString}");
        Console.WriteLine($"Exception : {notification.ExceptionString}");
        await _telegramServices.NotifyExceptionAsync(notification.ExceptionString!);
    }
}
