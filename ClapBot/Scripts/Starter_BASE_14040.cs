using System;
using System.IO;
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

    private static string _rootDirectory = string.Empty;
    /// <summary>
    /// Root directory of project
    /// </summary>
    public static string RootDirectory
    {
      get
      {
        if (_rootDirectory == string.Empty)
        {
          string rawPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Remove(0, 6);
          string newPath = string.Empty;
          bool hasFoundRoot = false;
          for (int i = rawPath.Split('\\').Length - 1; i >= 0; i--)
          {
            string newSection = rawPath.Split('\\')[i];
            if (newSection != "ClapBot" && !hasFoundRoot)
              continue;
            hasFoundRoot = true;
            newPath = newPath.Insert(0, newSection + '\\');
          }
          _rootDirectory = newPath;
        }

        return _rootDirectory;
      }
    }
    /// <summary>
    /// Bot Token
    /// </summary>
    private string Token
    {
      get
      {
        return System.IO.File.ReadAllText(RootDirectory + @"\Data\Token.txt");
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
      ClientConsole.Log("Connecting to discord");
      await Client.LoginAsync(TokenType.Bot, Token);
      await Client.StartAsync();
    }
  }
}
