using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace ClapBot
{
  static class ClientConsole
  {
    private static string MessageStart
    { get { return $"<{DateTime.Now}>"; } }

    public static async Task Log(LogMessage message)
    {
      await Task.Delay(0);
      Console.WriteLine($"{MessageStart} {message.Source}: {message.Message}");
    }

    public static void Log(string message)
    {
      Console.WriteLine($"{MessageStart} {message}");
    }

    public static void Log(SocketUserMessage message)
    {
      Console.WriteLine($"{MessageStart} Received new message from {message.Author.Username} #{message.Author.Discriminator} saying \"{message.Content}\" in {message.Channel.Name}");
    }
  }
}
