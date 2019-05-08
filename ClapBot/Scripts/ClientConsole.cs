using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using ClapBot.DataTypes;
using Discord;
using Discord.WebSocket;

namespace ClapBot
{
  /// <summary>
  /// Contains all logic for displaying information to console.
  /// </summary>
  static class ClientConsole
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
      await Log(message, true);
    }

    /// <summary>
    /// Takes information and sends it to the console and saves it a log file.
    /// </summary>
    /// <param name="message">Message to be sent to the log</param>
    /// <returns></returns>
    public static async Task Log(LogMessage message, bool saveToLog = true)
    {
      string output =
        $"{MessageStart} " +
        $"{message.Source}: " +
        $"{message.Message}";

      Console.WriteLine(output);
      if (saveToLog)
        await SaveSystem.AddToSaveLog(output);
    }


    /// <summary>
    /// Takes socket user message and sends it back to the log as a log message
    /// </summary>
    /// <param name="message">Users message</param>
    /// <returns></returns>
    public static async Task Log(SocketUserMessage message)
    {
      await Log(new LogMessage(LogSeverity.Info, 
        "Message", 
        $"{message.Author.Username} id-{message.Author.Id} said \"{message.Content}\""));
    }

    public static async Task Log(ClientMessage message)
    {
      Console.WriteLine(message.ToString());
      await SaveSystem.AddToSaveLog(message.ToString());
    }
  }
}