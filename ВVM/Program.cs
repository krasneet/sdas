using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ВVM
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var botClient = new TelegramBotClient("7640231439:AAHiU8z1qCp9OxTvP92YTe6xWMdobqyhrnY");

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;

            var me = await botClient.GetMeAsync(cancellationToken);
            Console.WriteLine($"Id бота: {me.Id}. Имя бота: {me.FirstName}.");

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
            };

            botClient.StartReceiving(UpdateHandler, ErrorHandler, receiverOptions, cancellationToken);

            Console.WriteLine("Бот запустился");
            Console.ReadLine();
            cts.Cancel();
        }

        private static async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message?.Text != null && update.Message.Text.StartsWith("/start"))
            {
                var keyboard = new InlineKeyboardMarkup(new[]
                {
                InlineKeyboardButton.WithUrl("Открыть тапалку", "https://krasneet.github.io/sdas/"),
                
            });

                await botClient.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text: "Нажмите на кнопку ниже что бы запустить тапалку:",
                    replyMarkup: keyboard,
                    cancellationToken: cancellationToken);
            }

        }


        private static Task ErrorHandler(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.Message);
            return Task.CompletedTask;
        }
    }
}