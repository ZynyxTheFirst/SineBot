using Sine;
using System;
class Program
{
    static void Main()
    {
        Bot bot = new Bot();
        bot.RunAsync().GetAwaiter().GetResult();
    }
}