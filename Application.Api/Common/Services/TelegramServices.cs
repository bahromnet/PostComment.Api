using MediatR;
using Telegram.Bot;

namespace Application.Api.Common.Services;

public class TelegramServices
{
    private readonly TelegramBotClient _botClient;
    private readonly long _chatId;

    public TelegramServices(string botToken, long chatId)
    {
        _botClient = new TelegramBotClient(botToken);
        _chatId = chatId;
    }
    public async Task NotifyExceptionAsync(string exceptionString)
    {
        await _botClient.SendTextMessageAsync(_chatId, $"Exception: {exceptionString}");
    }

    public async Task NotifyPostCreated(string post, string userName)
    {
        await _botClient.SendTextMessageAsync(_chatId, $"User : {userName},\nPost : {post}");
    }
}
