using Discord;
using Discord.Rest;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClapBot
{
  static class ActionLog
  {
    private static string MessageStart
    { get { return $"<{DateTime.Now}>"; } }

    public static async Task ClientLog(LogMessage message)
    {
      await Task.Delay(0);
      Console.WriteLine($"{MessageStart} {message.Source}: {message.Message}");
    }

    public static void ClientLog(string message)
    {
      Console.WriteLine($"{MessageStart} {message}");
    }

    public static void ClientLog(SocketUserMessage message)
    {
      Console.WriteLine($"{MessageStart} Received new message from {message.Author.Username} #{message.Author.Discriminator} saying \"{message.Content}\" in {message.Channel.Name}");
    }
  }
}
