using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace ClapBot
{
  static class ClientConsole
  {
    public static LogSeverity _logSeverity;

    private static string MessageStart
    { get { return $"<{DateTime.Now}>"; } }

    public static async Task Log(LogMessage message)
    {
      string output = 
        $"{MessageStart} " +
        $"{message.Source}: " +
        $"{message.Message}";

      Console.WriteLine(output);
      await SaveSystem.AddToSaveLog(output);
    }

    public static async Task Log(SocketUserMessage message)
    {
      await Log(new LogMessage(LogSeverity.Info, 
        "Message", 
        $"{message.Author.Username} id-{message.Author.Id} said \"{message.Content}\""));
    }
  }
}
