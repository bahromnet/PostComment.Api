using Application.Api.Common.Behaviours;
using Application.Api.Common.Interfaces;
using Application.Api.Common.Models.TelegramBot;
using Application.Api.Common.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Api;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var telegramSettings = configuration.GetSection("TelegramBotSettings").Get<TelegramBotSettings>();
        services.AddSingleton(new TelegramServices(telegramSettings.BotToken, telegramSettings.ChatId));

        //services.AddScoped<INotificationHandler<ExceptionNotification>, ExceptionNotificationHandle>();


        services.AddScoped<IHashPassword, HashPassword>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        });

        return services;
    }
}
