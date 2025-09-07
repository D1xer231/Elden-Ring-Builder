using Telegram.Bot;
using System;

namespace Elden_Ring_Builder.telegram
{

    class Program
    {
        private async Task Main()
        {
            var bot = new TelegramBotClient("YOUR_BOT_TOKEN");
            var me = await bot.GetMe();
            Console.WriteLine($"Hello, World! I am user {me.Id} and my name is {me.FirstName}.");
        }
    }
}