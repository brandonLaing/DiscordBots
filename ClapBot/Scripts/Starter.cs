using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace ClapBot
{
  class Starter
  {
    #region Core Variables
    private static DiscordSocketClient _client;
    /// <summary>
    /// Bots client that it uses
    /// </summary>
    public static DiscordSocketClient Client
    {
      get
      {
        return _client;
      }
      private set
      {
        _client = value;
      }
    }

    private static CommandService _commands;
    public static CommandService Commands
    {
      get
      {
        return _commands;
      }
      private set
      {
        _commands = value;
      }
    }

    public static string[] PriorityIds = { "2871", "6188", "5831"};
    #endregion
    /// <summary>
    /// Bot Token
    /// </summary>
    private static string Token
    {
      get
      {
        return "NTcyODY5NjAyMDMyNDg0Mzgz.XMvVoQ.MeWYkw1Ti949gLhaM2C1JSvcF7Q";
      }
    }

    static void Main(string[] args)
    {
      new Starter()
        .Main()
        .GetAwaiter()
        .GetResult();
    }

    private async Task Main()
    {
      InitilizeVariables();

      Client.MessageReceived += MessageHandler.ClientMessageRecived;

      await Commands.AddModulesAsync(Assembly.GetEntryAssembly(), null);

      Client.Ready += SetGame;
      Client.Log += ClientConsole.Log;
      Client.Disconnected += Reconnect;
      await Connect();

      await Task.Delay(-1);
    }

    /// <summary>
    /// Sets up the base variables for client and commands
    /// </summary>
    private void InitilizeVariables()
    {
      Client = new DiscordSocketClient(new DiscordSocketConfig
      {
        LogLevel = LogSeverity.Debug
      });

      Commands = new CommandService(new CommandServiceConfig
      {
        CaseSensitiveCommands = false,
        DefaultRunMode = RunMode.Async,
        LogLevel = LogSeverity.Debug,
        IgnoreExtraArgs = true
      });
    }

    /// <summary>
    /// Sets the game that the bot is playing
    /// </summary>
    /// <returns></returns>
    private async Task SetGame()
    {
      await Client.SetGameAsync($"with the {new Emoji("👏")} ", "", ActivityType.Playing);
    }

    /// <summary>
    /// Attempts to reconnect to the server if the bot has been disconnected
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    private async Task Reconnect(Exception exception)
    {
      ClientConsole.Log($"Disconnected from discord. Exception : {exception.Message}");
      await Task.Delay(1);
    }

    private async Task Connect()
    {
      if (Token == string.Empty)
      {
        ClientConsole.Log("Exited due to no key being set");
        return;
      }

      ClientConsole.Log("Connecting to discord");
      await Client.LoginAsync(TokenType.Bot, Token);
      await Client.StartAsync();
    }
  }
}
