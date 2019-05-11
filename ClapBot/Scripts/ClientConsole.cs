using System;
using System.Threading.Tasks;
using DiscordBots.DataTypes;
using Discord;

namespace ClapBot
{
  /// <summary>
  /// Contains all logic for displaying information to console.
  /// </summary>
  public static class ClientConsole
  {
    /// <summary>
    /// Start of the message. Constant for all information send to the console
    /// </summary>
    private static string MessageStart
    { get { return $"<{DateTime.Now}>"; } }

    /// <summary>
    /// Takes information and sends it to the console and saves it a log file.
    /// </summary>
    /// <param name="message">Message to be sent to the log</param>
    /// <returns></returns>
    public static async Task Log(LogMessage message)
    {
      string output =
        $"{MessageStart} " +
        $"{message.Source}: " +
        $"{message.Message}";

      Console.WriteLine(output);
      await SaveSystem.AddToSaveLog(output);
    }

    /// <summary>
    /// Takes a users message and displays it the console and saves it to a log
    /// </summary>
    /// <param name="message">Received user message</param>
    /// <returns></returns>
    public static async Task Log(string source, ClientMessage message)
    {
      Console.WriteLine($"{MessageStart} {source}: {message.ToString()}");
      await SaveSystem.AddToSaveLog($"{MessageStart} {source}: {message.ToString()}");
    }

    public static async Task Log(string source, string message)
    {
      Console.WriteLine($"{MessageStart} {source}: {message}");
      await SaveSystem.AddToSaveLog($"{MessageStart} {source}: {message.ToString()}");
    }

    public static async Task Log(CommandMessage message)
    {
      Console.WriteLine($"{MessageStart} Command: {message.ToString()}");
      await SaveSystem.AddToSaveLog($"{MessageStart} Command: {message.ToString()}");
    }
  }
}