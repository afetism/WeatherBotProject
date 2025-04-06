using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using WeatherBot.API.DTOs;

namespace WeatherBot.API.Services;



    public class TelegramListenerService:BackgroundService
    {


          private readonly ITelegramBotClient _botClient;
          private readonly IServiceScopeFactory _scopeFactory;
          private readonly ILogger<TelegramListenerService> _logger;

         public TelegramListenerService( ITelegramBotClient botClient, IServiceScopeFactory scopeFactory, ILogger<TelegramListenerService> logger)
         {
                 _botClient = botClient;
                 _scopeFactory = scopeFactory;
                 _logger = logger;
         }          

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = new[] { UpdateType.Message }
        };

        _botClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken: stoppingToken
        );

        _logger.LogInformation("Telegram bot listener başladı...");
        return Task.CompletedTask;
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message is not { Text: { } messageText }) return;

        var telegramId = update.Message.Chat.Id;
        var username = update.Message.Chat.Username ?? "Unknown";

        string city = "";

        if (messageText.StartsWith("/weather"))
        {
            var parts = messageText.Split(" ");
            if (parts.Length >= 2)
                city = parts[1];
        }

        if (!string.IsNullOrWhiteSpace(city))
        {
            using var scope = _scopeFactory.CreateScope();
            var weatherService = scope.ServiceProvider.GetRequiredService<IWeatherService>();

            await weatherService.ProcessUserWeatherRequestAsync(new WeatherRequestDto
            {
                TelegramId = telegramId,
                City = city
            });
        }
        else
        {
            await botClient.SendTextMessageAsync(
                chatId: telegramId,
                text: "Şəhəri belə göndər: /weather Baku",
                cancellationToken: cancellationToken
            );
        }
    }

    private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Telegram bot error:");
        return Task.CompletedTask;
    }

}

